using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing comments.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IProductRepository _productRepository;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		/// <summary>
		/// Initializes a new instance of the <see cref="CommentController"/> class.
		/// </summary>
		/// <param name="commentRepository">The comment repository to retrieve and manage comment</param>
		/// <param name="mapper">The AutoMapper instance for object mapping.</param>
		/// <param name="userRepository">The user repository to retrieve and manage user</param>
		/// <param name="productRepository">The product repository to retrieve and manage product</param>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		public CommentController(ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepository, 
			IProductRepository productRepository, IAuthorizationMiddleware authorizationMiddleware)
		{
			_commentRepository = commentRepository;
			_mapper = mapper;
			_userRepository = userRepository;
			_productRepository = productRepository;
			_authorizationMiddleware = authorizationMiddleware;
		}
        #region Get Methods
        /// <summary>
        /// Get all comments from the database.
        /// </summary>
        /// <returns>Returns a list of all comments</returns>
        /// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
        /// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
        /// <exception cref="NotFoundException">Thrown when no comments are found in the database.</exception>
        [HttpGet]
		[ProducesResponseType(200, Type = typeof(List<CommentDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllComments()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "COM-2001");
			
			var comments = _mapper.Map<List<CommentDTO>>(await _commentRepository.GetAllComments());
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
			
			if (comments.Count == 0)
				throw new NotFoundException("There is no comments in the database, please try again", "COM-1401");
			
			return Ok(comments);
		}

		/// <summary>
		/// Get an comment based on the ID.
		/// </summary>
		/// <param name="id">The ID of the comment.</param>
		/// <returns>Returns the comment with the specified ID.</returns>
		/// <exception cref="NotFoundException">Thrown when the comment with the specified ID doesn't exist.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(List<CommentDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCommentById(int id)
		{
			if (!await _commentRepository.CommentExists(id))
				throw new NotFoundException($"The comment with the ID : {id} doesn't exist", "COM-1402");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
			
			var comment = _mapper.Map<CommentDTO>(await _commentRepository.GetCommentById(id));
			return Ok(comment);
		}

		/// <summary>
		/// Get all comments made by a specific user.
		/// </summary>
		/// <param name="id">The ID of the user.</param>
		/// <returns>Returns a list of comments made by the user.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when no comments are found for the specified user.</exception>
		[HttpGet("{id}/user")]
		[ProducesResponseType(200, Type = typeof(List<CommentDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCommentsByUser(int id)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "COM-2001");

			// Retrieve comments made by the specified user
			var comments = _mapper.Map<List<CommentDTO>>(await _commentRepository.GetCommentByUser(id));

			// Check if any comment were found
			if (comments.Count == 0)
				throw new NotFoundException($"There is no comments of the user with the ID : {id}", "COM-1403");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "COM-1101");

			return Ok(comments);
		}

		/// <summary>
		/// Get all comments associated with a specific product.
		/// </summary>
		/// <param name="id">The ID of the product.</param>
		/// <returns>Returns a list of comments associated with the product.</returns>
		/// <exception cref="NotFoundException">Thrown when no comments are found for the specified product.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		[HttpGet("{id}/product")]
		[ProducesResponseType(200, Type = typeof(List<CommentDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCommentsByProduct(int id)
		{
			// Retrieve comments associated with the specified product
			var comments = _mapper.Map<List<CommentDTO>>(await _commentRepository.GetCommentByProduct(id));

			// Check if any comment were found
			if (comments.Count == 0)
				throw new NotFoundException($"There is no comments of the product with the ID : {id}", "COM-1404");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "COM-1101");

			return Ok(comments);
		}

		#endregion

		#region Post Methods
		/// <summary>
		/// Create a new comment.
		/// </summary>
		/// <param name="createCommentDto">The data to create the comment.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or the provided comment data is incomplete.</exception>
		/// <exception cref="NotFoundException">Thrown when the specified user or product does not exist.</exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO createCommentDto)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin or customer role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "COM-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "COM-1101");

			// Check if the provided comment data is complete
			if (createCommentDto == null)
				throw new BadRequestException("There is missing fields, please try again with some data", "COM-1102");

			// Retrieve the user and product associated with the comment
			var user = await _userRepository.GetUserById(createCommentDto.UserId);
			var product = await _productRepository.GetProductById(createCommentDto.ProductId);

			// Check if the user and product exist
			if (user == null)
				throw new NotFoundException("The user with ID : " + createCommentDto.UserId + " doesn't exist", "COM-1405");
			if (product == null)
				throw new NotFoundException("The product with ID : " + createCommentDto.ProductId + " doesn't exist", "COM-1406");

			// Create the comment using the repository
			await _commentRepository.CreateComment(createCommentDto);
			return StatusCode(201);
		}

		#endregion

		#region Put Methods
		/// <summary>
		/// Update an existing comment.
		/// </summary>
		/// <param name="id">The ID of the comment to update.</param>
		/// <param name="updateCommentDto">The updated comment data.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or the provided comment update data is incomplete.</exception>
		/// <exception cref="NotFoundException">Thrown when the comment with the specified ID doesn't exist.</exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDTO updateCommentDto)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "COM-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "COM-1101");

			// Check if the provided comment update data is complete
			if (updateCommentDto == null)
				throw new BadRequestException("There is missing fields, please try again with some data", "COM-1102");

			// Check if the comment with the given ID exists
			if (!await _commentRepository.CommentExists(id))
				throw new NotFoundException($"The comment with the ID : {id} doesn't exist", "COM-1402");

			// Update the comment using the repository
			await _commentRepository.UpdateCommentById(updateCommentDto, id);
			return Ok();
		}

		#endregion

		#region Delete Methods
		/// <summary>
		/// Delete an existing comment.
		/// </summary>
		/// <param name="id">The ID of the comment to delete.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the comment with the specified ID doesn't exist.</exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteComment(int id)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "COM-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "COM-1101");

			// Check if the comment with the given ID exists
			if (!await _commentRepository.CommentExists(id))
				throw new NotFoundException($"The comment with the ID : {id} doesn't exist", "COM-1402");

			// Delete the comment using the repository
			await _commentRepository.DeleteCommentById(id);
			return Ok();
		}

		#endregion
	}
}
