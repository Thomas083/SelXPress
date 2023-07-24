using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.DTO.MarkDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IMapper _mapper;
		private readonly IMarkRepository _markRepository;
		private readonly IProductRepository _productRepository;
		private readonly IUserRepository _userRepository;

		public CommentController(ICommentRepository commentRepository, IMapper mapper,
			IMarkRepository markRepository, IProductRepository productRepository, IUserRepository userRepository)
		{
			_commentRepository = commentRepository;
			_mapper = mapper;
			_markRepository = markRepository;
			_productRepository = productRepository;
			_userRepository = userRepository;
		}
		/// <summary>
		/// GET: api/<CommentController>
		/// Get all comments
		/// </summary>
		/// <returns>Return an Array of all comment</returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<CommentDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllComments()
		{
			var comments = _mapper.Map<List<CommentDTO>>(await _commentRepository.GetAllComments());
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1000");
			if (comments.Count == 0)
				throw new NotFoundException("There is no comments in the database, please try again", "COM-1001");
			return Ok(comments);
		}

		/// <summary>
		/// GET api/<CommentController>/5
		/// Get a comment by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific comment</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(List<CommentDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCommentById(int id)
		{
			if (!await _commentRepository.CommentExists(id))
				throw new NotFoundException("The comment with the id " + id + " doesn't exist", "COM-1003");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1004");
			var comment = _mapper.Map<CommentDTO>(await _commentRepository.GetCommentById(id));
			return Ok(comment);
		}

		/// <summary>
		/// Get all the comments of a user
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("{id}/user")]
		[ProducesResponseType(200, Type = typeof(List<CommentDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCommentsByUser(int id)
		{
			var comments = _mapper.Map<List<CommentDTO>>(await _commentRepository.GetCommentByUser(id));
			if (comments.Count == 0)
				throw new NotFoundException("There is no comments of the user with the id : " + id, "COM-1005");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1006");
			return Ok(comments);
		}
		
		/// <summary>
		/// Get comments of a product
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
				throw new NotFoundException("There is no comments of the product with the id : " + id, "COM-1007");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1008");
			return Ok(comments);
		}

		/// <summary>
		/// Create comment
		/// </summary>
		/// <param name="createCommentDto"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO createCommentDto)
		{
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1009");
			if (createCommentDto == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "COM-1010");
			var user = await _userRepository.GetUserById(createCommentDto.UserId);
			if (user == null)
				throw new NotFoundException("The user with the id : " + createCommentDto.UserId + "doesn't exist", "COM-1011");
			await _commentRepository.CreateComment(createCommentDto);
			
			return Ok();
		}

		/// <summary>
		/// Update comment
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updateCommentDto"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDTO updateCommentDto)
		{
			return Ok();
		}

		/// <summary>
		/// DELETE api/<CommentController>/5
		/// Delete a comment
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteComment(int id)
		{
			return Ok();
		}
	}
}
