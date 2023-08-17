using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.CartDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartRepository _cartRepository;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		public CartController(ICartRepository cartRepository, IAuthorizationMiddleware authorizationMiddleware)
		{
			_cartRepository = cartRepository;
			_authorizationMiddleware = authorizationMiddleware;
		}
		
		/// <summary>
		/// Method to get all the carts of the database
		/// </summary>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Cart>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllCarts()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			
			var carts = await _cartRepository.GetAllCarts();
			if (carts.Count == 0)
				throw new NotFoundException("There is no carts in the database, please try again", "CRT-");
			
			return Ok(carts);
		}

		/// <summary>
		/// Method to get the cart based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Cart))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCartById(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
            
			if (!await _cartRepository.CartExists(id))
				throw new NotFoundException("The cart with the id :" + id + " doesn't exist", "CRT-");
			
			var cart = await _cartRepository.GetCartById(id);
			return Ok(cart);
		}
		
		/// <summary>
		/// Method to get all the carts based on the user id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpGet("{id}/user")]
		[ProducesResponseType(200, Type = typeof(List<Cart>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCartByUserId(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			
			var carts = await _cartRepository.GetCartsByUserId(id);
			if (carts.Count == 0)
				throw new NotFoundException("There is no carts for the user with the id : " + id, "CRT-");
			
			return Ok(carts);
		}

		/// <summary>
		/// Method to create a cart	(accessible necessary by admin role)
		/// </summary>
		/// <param name="cartDto"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateCartByAdmin([FromBody] CreateCartByAdminDto cartDto)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
            
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			
			if (cartDto == null)
				throw new BadRequestException("There is missing fields, please try again with some data", "CRT-");
			
			await _cartRepository.CreateCartByAdmin(cartDto);
			return Ok();
		}
		
		[HttpPost("me")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateCartByUser([FromBody] CreateCartDto cartDto)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) && !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			string? email = HttpContext.Request.Headers["EmailHeader"];
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			
			if (cartDto == null)
				throw new BadRequestException("There is missing fields, please try again with some data", "CRT-");
			
			await _cartRepository.CreateCartByUser(cartDto,email);
			return Ok();
		}

		/// <summary>
		/// Method to update the cart based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cartDto"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateCart(int id, [FromBody] UpdateCartDto cartDto)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
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
		/// Method to delete a cart based on the id
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
		public async Task<IActionResult> DeleteCart(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CRT-");
			
			if (!await _cartRepository.CartExists(id))
				throw new NotFoundException("The cart with the id : " + id + " doesn't exist", "CRT-");
			
			await _cartRepository.DeleteCart(id);
			return Ok();
		}
	}
}
