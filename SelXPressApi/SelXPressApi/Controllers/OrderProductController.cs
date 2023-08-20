using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderProductController : ControllerBase
	{
		private readonly IAuthorizationMiddleware _authorizationMiddleware;
		private readonly IOrderProductRepository _orderProductRepository;
		private readonly IMapper _mapper;

		public OrderProductController(IAuthorizationMiddleware authorizationMiddleware, IOrderProductRepository orderProductRepository, IMapper mapper)
		{
			_authorizationMiddleware = authorizationMiddleware;
			_orderProductRepository = orderProductRepository;
			_mapper = mapper;
		}
		/// <summary>
		/// GET: api/<OrderProductController>
		/// Get all order products
		/// </summary>
		/// <returns>Return an Array of all orders</returns>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrderProducts()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ODP-2001");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ODP-1101");

			// Retrieve orders from the repository
			var orderProducts = await _orderProductRepository.GetAllOrderProducts();
			if (orderProducts.Count == 0)
				throw new NotFoundException("There is no order product in the database, please try again", "ODP-1401");
			return Ok(orderProducts);
		}

		/// <summary>
		/// Get all order products associated with the current user.
		/// Retrieve order products based on the email in the header.
		/// </summary>
		/// <returns>Returns a list of order products associated with the user.</returns>
		[HttpGet("me")]
		[ProducesResponseType(200, Type = typeof(OrderProductDTO))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrderProductByUser()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			if (!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "ODP-2001");

			string? email = HttpContext.Request.Headers["EmailHeader"]; // Email of the user

			if (string.IsNullOrEmpty(email))
				throw new BadRequestException("The email is missing in the request header", "ODP-1102");

			if (!ModelState.IsValid)
				throw new BadRequestException("The model is invalid, a bad request occurred", "ODP-1101");

			// todo: Implement the logic to retrieve order products associated with the user's email
			// You can use the email to query the database for order products
			var orderProducts = await _orderProductRepository.GetOrderProductsByUser(email);
			if (orderProducts.Count == 0)
				throw new NotFoundException("There is no order product in the database, please try again", "ODP-1401");
			return Ok(orderProducts);
		}


		/// <summary>
		/// GET api/<OrderProductController>/5
		/// Get a specific order product
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific order product</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(OrderProductDTO))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrderProduct(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ODP-2001");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ODP-1101");

			// Retrieve order product from the repository
			var orderProduct = await _orderProductRepository.GetOrderProductById(id);
			if (orderProduct == null)
				throw new NotFoundException("There is no order product in the database, please try again", "ODP-1401");
			return Ok(orderProduct);
		}

		// <summary>
		/// Create a new order product
		/// </summary>
		/// <param name="order"></param>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateOrderProduct([FromBody] CreateOrderProductDTO order)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ODP-2001");

			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ODP-1102");
			await _orderProductRepository.CreateOrderProduct(order);
			return StatusCode(201);
		}

		/// <summary>
		/// PUT api/<OrderProductController>/5
		/// Modify a specific order product
		/// </summary>
		/// <param name="id"></param>
		/// <param name="order"></param>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		public async Task<IActionResult> UpdateOrderProduct(int id, [FromBody] UpdateOrderProductDTO order)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ODP-2001");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ODP-1101");

			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ODP-1102");

			if (!await _orderProductRepository.OrderProductExists(id))
				throw new NotFoundException("The order product with the id : " + id + " doesn't exist ", "ODP-1401");

			await _orderProductRepository.UpdateOrderProduct(id, order);
			return Ok();
		}

		/// <summary>
		/// Delete a specific order product
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteOrderProduct(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ODP-2001");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ODP-1101");
			if (!await _orderProductRepository.OrderProductExists(id))
				throw new NotFoundException("The order product with the id : " + id + " doesn't exist ", "ODP-1401");

			await _orderProductRepository.DeleteOrderProduct(id);
			return Ok();
		}
	}
}
