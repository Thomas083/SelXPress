using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Interfaces;
using SelXPressApi.Exceptions;
using SelXPressApi.Helper;
using SelXPressApi.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing users.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly FirebaseAuthManager _authManager;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserController"/> class.
		/// </summary>
		/// <param name="userRepository">The user repository to retrieve and manage users.</param>
		/// <param name="mapper">The AutoMapper instance for object mapping.</param>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		public UserController(IUserRepository userRepository, IMapper mapper, IAuthorizationMiddleware authorizationMiddleware)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_authManager = new FirebaseAuthManager();
			_authorizationMiddleware = authorizationMiddleware;
		}

		#region Get Methods
		/// <summary>
		/// Get all users.
		/// </summary>
		/// <returns>Returns an array of all users.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when no users are found in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<UserDto>))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetUsers()
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "USR-1101");

			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to access this data", "USR-2001");

			// Create the user using the repository
			var users = _mapper.Map<List<UserDto>>(await _userRepository.GetAllUsers());

			// Check if any users were found
			if (users.Count == 0)
				throw new NotFoundException("There are no users in the database, please try again", "USR-1401");

			return Ok(users);
		}

		/// <summary>
		/// Get a user by their email.
		/// </summary>
		/// <returns>Returns the user with the specified email.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the user with the specified email doesn't exist.</exception>
		/// <exception cref="UnauthorizedException">Thrown when the user is not authorized to perform this operation.</exception>
		[HttpGet("email")]
		[ProducesResponseType(200, Type = typeof(UserDto))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetUser()
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Get the email from the response headers
			string? email = HttpContext.Response.Headers["EmailHeader"];

			// Check if the user with the given email exists
			if (!await _userRepository.UserExistsEmail(email))
				throw new NotFoundException($"The user with the email: {email} doesn't exist", "USR-1403");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "USR-1101");

			// Retrieve and return the user with the specified email
			var user = _mapper.Map<UserDto>(await _userRepository.GetUserByEmail(email));
			return Ok(user);
		}
        #endregion


        #region Post Methods
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="newUser">The user information to create.</param>
        /// <returns>Returns a status code indicating the result of the operation.</returns>
        /// <exception cref="BadRequestException">Thrown when the provided user data is incomplete.</exception>
        /// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to create a seller or operator user</exception>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto newUser)
        {
            // Check if the provided user data is complete
            if (newUser.Username == null || newUser.Email == null || newUser.Password == null || newUser.RoleId == 0)
                throw new BadRequestException("There are missing fields, please try again with some data", "USR-1102");

            //Check if the provided RoleId id equals to 3 or 2 and check if the user is authorized to do this operation
            if (newUser.RoleId == 3 || newUser.RoleId == 2)
            {
                await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
                if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
                    throw new ForbiddenRequestException("You are not authorized to do this operation", "USR-2001");
            }

            // Create user with email and password using the authentication manager (firebase)
            string firebaseToken = await _authManager.CreateWithEmailAndPasswordAsync(newUser.Email, newUser.Password);

			if (!string.IsNullOrEmpty(firebaseToken))
			{
				// The authentication succeeded, proceed to create the user using the repository
				await _userRepository.CreateUser(newUser);
			}
			else
			{
				// The authentication failed, throw an exception
				throw new BadRequestException("The user could not be created, please try again", "USR-1104");
			}

            return StatusCode(201);
        }

		/// <summary>
		/// Authenticate and log in a user using email and password.
		/// </summary>
		/// <param name="loginDto">The login information.</param>
		/// <returns>Returns a token and user email upon successful login.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		[HttpPost("auth/login")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "USR-1101");

			// Authenticate and retrieve a token using the authentication manager
			string token = await _authManager.LoginWithEmailAndPasswordAsync(loginDto.Email, loginDto.Password);
			return Ok(new { Token = token, Email = loginDto.Email });
		}

		/// <summary>
		/// Refresh the authentication token using a refresh token.
		/// </summary>
		/// <param name="loginDto">The login information containing the refresh token.</param>
		/// <returns>Returns a new token and user email upon successful token refresh.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="InternalServerErrorException">Thrown when an internal server error occurs.</exception>
		[HttpPost("refreshToken")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetRefreshToken([FromBody] LoginDto loginDto)
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "USR-1101");

			// Refresh the authentication token using the refresh token
			string token = await _authManager.LoginWithEmailAndPasswordRefreshAsync(loginDto.Email, loginDto.Password);
			return Ok(new { Token = token, Email = loginDto.Email });
		}
		#endregion


		#region Put Methods
		/// <summary>
		/// Update an existing user's information based on their email.
		/// </summary>
		/// <param name="userUpdate">The updated user data.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or the provided user update data is incomplete.</exception>
		/// <exception cref="NotFoundException">Thrown when the user with the specified email doesn't exist.</exception>
		[HttpPut("email")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO userUpdate)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Retrieve the user's email from the response headers
			string? email = HttpContext.Response.Headers["EmailHeader"];

			// Check if the provided user update data is complete
			if (userUpdate == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "USR-1102");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "USR-1101");

			// Check if the user with the given email exists
			if (!await _userRepository.UserExistsEmail(email))
				throw new NotFoundException($"The user with the email : {email} doesn't exist", "USR-1403");

			// Update the user's information using the repository
			await _userRepository.UpdateUser(userUpdate, email);
			return Ok();
		}

        /// <summary>
        /// Update an existing user's information by operator based on their Id.
        /// </summary>
        /// <param name="userUpdate">The updated user data.</param>
        /// <param name="id">The updated user id.</param>
        /// <returns>Returns a status code indicating the result of the operation.</returns>
        /// <exception cref="BadRequestException">Thrown when the model state is invalid or the provided user update data is incomplete.</exception>
        /// <exception cref="NotFoundException">Thrown when the user with the specified email doesn't exist.</exception>
        [HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateUserById([FromBody] UpdateUserDTO userUpdate, int id)
		{
			// check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// check if the user is an admin
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "USR-2001");

			// check if the provided user update data is complete
			if (userUpdate == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "USR-1102");

			// check if the model state is valid
			if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occurred", "USR-1101");

			// check if the user with the given id exists
			if (!await _userRepository.UserExists(id))
                throw new NotFoundException($"The user with the id : {id} doesn't exist", "USR-1402");

			// update the user's information using the repository
			await _userRepository.UpdateUserById(userUpdate, id);
			return Ok();
		}
		#endregion

		#region Delete Methods
		/// <summary>
		/// Delete an existing user based on their ID.
		/// </summary>
		/// <param name="id">The ID of the user to delete.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not allowed to perform this operation.</exception>
		/// <exception cref="NotFoundException">Thrown when the user with the specified ID doesn't exist.</exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteUser(int id)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not allowed to do this operation", "USR-2001");

			// Check if the user with the given ID exists
			if (!await _userRepository.UserExists(id))
				throw new NotFoundException($"The user with the ID : {id} doesn't exist", "USR-1402");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "USR-1101");

			// Delete the user using the repository
			await _userRepository.DeleteUser(id);
			return Ok();
		}
		#endregion

	}
}
