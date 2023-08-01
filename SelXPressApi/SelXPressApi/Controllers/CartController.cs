using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.CartDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartRepository _cartRepository;

		public CartController(ICartRepository cartRepository)
		{
			_cartRepository = cartRepository;
		}
		
		/// <summary>
		/// GET: api/<CartController>
		/// Get all carts
		/// </summary>
		/// <returns>Return an Array of all cart</returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Cart>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllCarts()
		{
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			var carts = await _cartRepository.GetAllCarts();
			if (carts.Count == 0)
				throw new NotFoundException("There is no carts in the database, please try again", "CRT-");
			return Ok(carts);
		}

		/// <summary>
		/// GET api/<CartController>/5
		/// Get a cart by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return the information of a specific cart</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Cart))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCartById(int id)
		{
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			if (!await _cartRepository.CartExists(id))
				throw new NotFoundException("The cart with the id :" + id + " doesn't exist", "CRT-");
			var cart = await _cartRepository.GetCartById(id);
			return Ok(cart);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpGet("{id}/user")]
		[ProducesResponseType(200, Type = typeof(List<Cart>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCartByUserId(int id)
		{
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			var carts = await _cartRepository.GetCartsByUserId(id);
			if (carts.Count == 0)
				throw new NotFoundException("There is no carts for the user with the id : " + id, "CRT-");
			return Ok(carts);
		}

		/// <summary>
		/// POST api/<CartController>
		/// Create new cart
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateCart([FromBody] CreateCartDto cartDto)
		{
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			if (cartDto == null)
				throw new BadRequestException("There is missing fields, please try again with some data", "CRT-");
			await _cartRepository.CreateCart(cartDto);
			return Ok();
		}

		/// <summary>
		/// PUT api/<CartController>/5
		/// Modify a cart
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateCart(int id, [FromBody] UpdateCartDto cartDto)
		{
			if (!await _cartRepository.CartExists(id))
				throw new NotFoundException("The cart with the id : " + id + " doesn't exist", "CRT-");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			if (cartDto == null)
				throw new BadRequestException("There is missing fields, please try again with some data", "CRT-");
			await _cartRepository.UpdateCart(cartDto, id);
			return Ok();
		}
		/// <summary>
		/// DELETE api/<CartController>/5
		/// Delete a cart
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteCart(int id)
		{
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			if (!await _cartRepository.CartExists(id))
				throw new NotFoundException("The cart with the id : " + id + " doesn't exist", "CRT-");
			await _cartRepository.DeleteCart(id);
			return Ok();
		}
	}
}
