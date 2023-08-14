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
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;
		private readonly IProductRepository _productRepository;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		public CommentController(ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepository, 
			IProductRepository productRepository, IAuthorizationMiddleware authorizationMiddleware)
		{
			_commentRepository = commentRepository;
			_mapper = mapper;
			_userRepository = userRepository;
			_productRepository = productRepository;
			_authorizationMiddleware = authorizationMiddleware;
		}
		/// <summary>
		/// Method to get all the comments of the database
		/// </summary>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
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
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
			var comments = _mapper.Map<List<CommentDTO>>(await _commentRepository.GetAllComments());
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
			
			if (comments.Count == 0)
				throw new NotFoundException("There is no comments in the database, please try again", "COM-1401");
			
			return Ok(comments);
		}

		/// <summary>
		/// Method to get a comment based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(List<CommentDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCommentById(int id)
		{
			if (!await _commentRepository.CommentExists(id))
				throw new NotFoundException("The comment with the id " + id + " doesn't exist", "COM-1402");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
			
			var comment = _mapper.Map<CommentDTO>(await _commentRepository.GetCommentById(id));
			return Ok(comment);
		}

		/// <summary>
		/// Get all the comments of a user based on the id user
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("{id}/user")]
		[ProducesResponseType(200, Type = typeof(List<CommentDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCommentsByUser(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
			var comments = _mapper.Map<List<CommentDTO>>(await _commentRepository.GetCommentByUser(id));
			
			if (comments.Count == 0)
				throw new NotFoundException("There is no comments of the user with the id : " + id, "COM-1403");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
			
			return Ok(comments);
		}
		
		/// <summary>
		/// Get all the comments of a product based on the product id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("{id}/product")]
		[ProducesResponseType(200, Type = typeof(List<CommentDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCommentsByProduct(int id)
		{
			var comments = _mapper.Map<List<CommentDTO>>(await _commentRepository.GetCommentByProduct(id));
			
			if (comments.Count == 0)
				throw new NotFoundException("There is no comments of the product with the id : " + id, "COM-1404");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
			
			return Ok(comments);
		}

		/// <summary>
		/// Method to create a comment
		/// </summary>
		/// <param name="createCommentDto"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO createCommentDto)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
			
			if (createCommentDto == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "COM-1102");
			
			var user = await _userRepository.GetUserById(createCommentDto.UserId);
			var product = await _productRepository.GetProductById(createCommentDto.ProductId);
			
			if (user == null)
				throw new NotFoundException("The user with the id : " + createCommentDto.UserId + "doesn't exist", "COM-1405");
			
			if (product == null)
				throw new NotFoundException(
					"The product with the id : " + createCommentDto.ProductId + " doesn't exist", "COM-1406");
			
			await _commentRepository.CreateComment(createCommentDto);
			return Ok();
		}

		/// <summary>
		/// Method to update a comment based on comment's id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updateCommentDto"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDTO updateCommentDto)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured","COM-1101");
			
			if (!await _commentRepository.CommentExists(id))
				throw new NotFoundException("The comment with the id " + id + " doesn't exist", "COM-1402");
			
			if (updateCommentDto == null)
				throw new BadRequestException("There is missing fields, please try again with some data", "COM-1102");
			
			await _commentRepository.UpdateCommentById(updateCommentDto, id);
			return Ok();
		}

		/// <summary>
		/// Method to delete a comment based on the comment id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteComment(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this opeartion", "todo");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
			
			if (!await _commentRepository.CommentExists(id))
				throw new NotFoundException("The comment with the id " + id + " doesn't exist", "COM-1402");
			
			await _commentRepository.DeleteCommentById(id);
			return Ok();
		}
	}
}
