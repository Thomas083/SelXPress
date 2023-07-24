using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentRepository _commentRepository;
		private readonly IMapper _mapper;

		public CommentController(ICommentRepository commentRepository, IMapper mapper)
		{
			_commentRepository = commentRepository;
			_mapper = mapper;
		}
		/// <summary>
		/// GET: api/<CommentController>
		/// Get all comments
		/// </summary>
		/// <returns>Return an Array of all comment</returns>
		[HttpGet]
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
		/// POST api/<CommentController>
		/// Create a new comment
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		/// <summary>
		/// PUT api/<CommentController>/5
		/// Modify a comment
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		/// <summary>
		/// DELETE api/<CommentController>/5
		/// Delete a comment
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
