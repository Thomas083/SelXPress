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
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly FirebaseAuthManager _authManager;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		public UserController(IUserRepository userRepository, IMapper mapper, IAuthorizationMiddleware authorizationMiddleware)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_authManager = new FirebaseAuthManager();
			_authorizationMiddleware = authorizationMiddleware;
		}

		/// <summary>
		/// Method to get all the users in the database
		/// </summary>
		/// <returns></returns>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<UserDto>))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetUsers()
		{
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "USR-1101");
            // check of the validity of the token
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			
			//check the role of the connected user
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to access at this data", "USR-2001");
			
			// get the data
            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetAllUsers());

            if (users.Count == 0)
	            throw new NotFoundException("There is no users in the database, please try again", "USR-1401");

            return Ok(users);
        }

		/// <summary>
		/// Get the user informations with the help of a firebase token
		/// </summary>
		/// <returns></returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("email")]
		[ProducesResponseType(200, Type = typeof(UserDto))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetUser()
		{
			// check if the token exists and create the email header
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			
			string? email = HttpContext.Response.Headers["EmailHeader"];
            if (!await _userRepository.UserExistsEmail(email))
            {
	            throw new NotFoundException("The user with the email : " + email + " doesn't exist", "USR-1403");
            }

            if (!ModelState.IsValid)
	            throw new BadRequestException("The model is wrong , a bad request occured", "USR-1101");
            
            var user = _mapper.Map<UserDto>(await _userRepository.GetUserByEmail(email));
            return Ok(user);
        }

		/// <summary>
		/// Create a new user
		/// </summary>
		/// <param name="newUser"></param>
		/// <returns></returns>
		/// <exception cref="BadRequestException"></exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserDto newUser)
		{
            if (newUser.Username == null || newUser.Email == null || newUser.Password == null || newUser.RoleId == 0)
            {
	            throw new BadRequestException("There are missing fields, please try again with some data", "USR-1102");
            }

            // create the user in our database (SQL)
            await _userRepository.CreateUser(newUser);
            
            // create the user in firebase
            await _authManager.CreateWithEmailAndPasswordAsync(newUser.Email, newUser.Password);
            return StatusCode(201);
		}

		/// <summary>
		/// Update the user based on his/her email
		/// </summary>
		/// <param name="userUpdate"></param>
		/// <returns></returns>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpPut("email")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO userUpdate)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			string? email = HttpContext.Response.Headers["EmailHeader"];
			if (userUpdate == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "USR-1102");

			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "USR-1101");

			if (!await _userRepository.UserExistsEmail(email))
				throw new NotFoundException("The user with the email : " + email + " doesn't exist", "USR-1403");
            
            await _userRepository.UpdateUser(userUpdate, email);
            return Ok();
		}

		/// <summary>
		/// Method to delete an user based on his/her user
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteUser(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not allowed to do this operation", "USR-2001");
			
			if (!await _userRepository.UserExists(id))
				throw new NotFoundException("The user with the id :" + id + " doesn't exist", "USR-1402");

			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong , a bad request occured", "USR-1101");
            
            await _userRepository.DeleteUser(id);
            return Ok();
		}

		/// <summary>
		/// Method for login the user
		/// </summary>
		/// <param name="loginDto"></param>
		/// <returns></returns>
		/// <exception cref="BadRequestException"></exception>
		[HttpPost("auth/login")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> login([FromBody] LoginDto loginDto)
		{
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "USR-1101");
			string token = await _authManager.LoginWithEmailAndPasswordAsync(loginDto.Email, loginDto.Password);
			return Ok(new {Token = token, Email = loginDto.Email});
		}
		
		/// <summary>
		/// Method to get the refresh token
		/// </summary>
		/// <param name="loginDto"></param>
		/// <returns></returns>
		/// <exception cref="BadRequestException"></exception>
		[HttpPost("refreshToken")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetRefreshToken([FromBody] LoginDto loginDto)
		{
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "USR-1101");
			string token = await _authManager.LoginWithEmailAndPasswordRefreshAsync(loginDto.Email, loginDto.Password);
			return Ok(new { Token = token, Email = loginDto.Email });
		}

	}
}
