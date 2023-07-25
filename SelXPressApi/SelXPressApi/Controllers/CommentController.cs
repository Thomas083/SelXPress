﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
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
		private readonly IUserRepository _userRepository;
		private readonly IProductRepository _productRepository;

		public CommentController(ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepository, 
			IProductRepository productRepository)
		{
			_commentRepository = commentRepository;
			_mapper = mapper;
			_userRepository = userRepository;
			_productRepository = productRepository;
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
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
			if (comments.Count == 0)
				throw new NotFoundException("There is no comments in the database, please try again", "COM-1401");
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
				throw new NotFoundException("The comment with the id " + id + " doesn't exist", "COM-1402");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
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
				throw new NotFoundException("There is no comments of the user with the id : " + id, "COM-1403");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
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
				throw new NotFoundException("There is no comments of the product with the id : " + id, "COM-1404");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
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
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "COM-1101");
			if (!await _commentRepository.CommentExists(id))
				throw new NotFoundException("The comment with the id " + id + " doesn't exist", "COM-1402");
			await _commentRepository.DeleteCommentById(id);
			return Ok();
		}
	}
}
