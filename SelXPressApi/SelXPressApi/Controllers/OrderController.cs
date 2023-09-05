using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing orders.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IAuthorizationMiddleware _authorizationMiddleware;
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderController"/> class.
		/// </summary>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		/// <param name="orderRepository">The order repository to retrieve and manage orders.</param>
		/// <param name="mapper">The AutoMapper instance for object mapping.</param>
		public OrderController(IAuthorizationMiddleware authorizationMiddleware, IOrderRepository orderRepository, IMapper mapper)
		{
			_authorizationMiddleware = authorizationMiddleware;
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		#region Get Methods
		/// <summary>
		/// Get all orders.
		/// </summary>
		/// <returns>Returns an array of all orders.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when there are no orders in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Order>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllOrders()
		{
			// Authorization Check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");

			// Model State Check
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ORD-1101");

			// Retrieve orders from the repository
			var orders = await _orderRepository.GetAllOrders();
			if (orders.Count == 0)
				throw new NotFoundException("There are no orders in the database, please try again", "ORD-1401");

			return Ok(orders);
		}

		/// <summary>
		/// Get a specific order.
		/// </summary>
		/// <param name="id">The ID of the order.</param>
		/// <returns>Returns the specific order with the specified ID.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the order with the specified ID doesn't exist.</exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Order))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrder(int id)
		{
			// Authorization Check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");

			// Model State Check
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ORD-1101");

			// Check if order exists
			if (!await _orderRepository.OrderExists(id))
				throw new NotFoundException("The order with ID : " + id + " doesn't exist", "ORD-1402");

			// Retrieve order from the repository
			var order = await _orderRepository.GetOrderById(id);
			return Ok(order);
		}

		[HttpGet("me")]
		[ProducesResponseType(200, Type = typeof(List<Order>))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrderByUser()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");

			string? email = HttpContext.Response.Headers["EmailHeader"];
			var orders = await _orderRepository.GetOrderByUser(email);
			if (orders.Count == 0)
				throw new NotFoundException("There is no orders for this user", "ORD-1403");
			
			return Ok(orders);
		}
		#endregion

		#region Post Method
		/// <summary>
		/// Create a new order.
		/// </summary>
		/// <param name="order">The order to create.</param>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO order)
		{
			// Authorization Check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");

			// Model validation
			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ORD-1102"); ;

			// Create order
			await _orderRepository.CreateOrder(order);
			return StatusCode(201);
		}
		#endregion

		#region Put Method
		/// <summary>
		/// Modify a specific order.
		/// </summary>
		/// <param name="id">The ID of the order to update.</param>
		/// <param name="order">The updated order data.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or the provided order update data is incomplete.</exception>
		/// <exception cref="NotFoundException">Thrown when the order with the specified ID doesn't exist.</exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDTO order)
		{
			// Authorization Check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");

			// Model validation
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ORD-1101");

			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ORD-1102");

			// Check if order exists
			if (!await _orderRepository.OrderExists(id))
				throw new NotFoundException("The order with ID " + id + " doesn't exist", "ORD-1401");

			// Update order
			await _orderRepository.UpdateOrder(id, order);
			return Ok();
		}
		#endregion

		#region Delete Method
		/// <summary>
		/// Delete a specific order.
		/// </summary>
		/// <param name="id">The ID of the order to delete.</param>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the order with the specified ID doesn't exist.</exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			// Authorization Check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");

			// Model validation
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ORD-1101");

			// Check if order exists
			if (!await _orderRepository.OrderExists(id))
				throw new NotFoundException("The order with ID " + id + " doesn't exist", "ORD-1401");

			// Delete order
			await _orderRepository.DeleteOrder(id);
			return Ok();
		}
		#endregion
	}
}
