using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using System.Collections.Generic;
using System.Threading.Tasks;

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

		#region GET Methods

		/// <summary>
		/// Get all order products
		/// </summary>
		/// <returns>Returns an array of all order products</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when there are no order products in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrderProducts()
		{
			// Authorization check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ODP-2001");

			// Model validation
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ODP-1101");

			// Retrieve order products from the repository
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
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when there are no order products associated with the user.</exception>
		[HttpGet("me")]
		[ProducesResponseType(200, Type = typeof(OrderProductDTO))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrderProductByUser()
		{
			// Authorization check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "ODP-2001");

			// Retrieve user's email from header
			string? email = HttpContext.Request.Headers["EmailHeader"]; // Email of the user

			if (string.IsNullOrEmpty(email))
				throw new BadRequestException("The email is missing in the request header", "ODP-1102");

			// Model validation
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
		/// Get a specific order product
		/// </summary>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when there are no order products associated with the user.</exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(OrderProductDTO))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrderProduct(int id)
		{
			// Authorization check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ODP-2001");

			// Model validation
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ODP-1101");

			// Retrieve order product from the repository
			var orderProduct = await _orderProductRepository.GetOrderProductById(id);
			if (orderProduct == null)
				throw new NotFoundException("There is no order product in the database, please try again", "ODP-1401");

			return Ok(orderProduct);
		}

		#endregion

		#region POST Method

		/// <summary>
		/// Create a new order product
		/// </summary>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateOrderProduct([FromBody] CreateOrderProductDTO order)
		{
			// Authorization check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ODP-2001");

			// Model validation
			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ODP-1102");

			await _orderProductRepository.CreateOrderProduct(order);
			return StatusCode(201);
		}

		#endregion

		#region PUT Method

		/// <summary>
		/// Update a specific order product
		/// </summary>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid or if there is a missing field</exception>
		/// <exception cref="NotFoundException">Thrown when there are no order products associated with the user.</exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		public async Task<IActionResult> UpdateOrderProduct(int id, [FromBody] UpdateOrderProductDTO order)
		{
			// Authorization check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ODP-2001");

			// Model validation
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ODP-1101");

			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ODP-1102");

			if (!await _orderProductRepository.OrderProductExists(id))
				throw new NotFoundException("The order product with the id : " + id + " doesn't exist ", "ODP-1401");

			await _orderProductRepository.UpdateOrderProduct(id, order);
			return Ok();
		}

		#endregion

		#region DELETE Method

		/// <summary>
		/// Delete a specific order product.
		/// </summary>
		/// <param name="id"></param>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the order product with the specified id doesn't exist.</exception>

		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteOrderProduct(int id)
		{
			// Authorization check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ODP-2001");

			// Model validation
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ODP-1101");

			if (!await _orderProductRepository.OrderProductExists(id))
				throw new NotFoundException("The order product with the id : " + id + " doesn't exist ", "ODP-1401");

			await _orderProductRepository.DeleteOrderProduct(id);
			return Ok();
		}

		#endregion
	}
}
