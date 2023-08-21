using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IAuthorizationMiddleware _authorizationMiddleware;
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public OrderController(IAuthorizationMiddleware authorizationMiddleware, IOrderRepository orderRepository, IMapper mapper)
		{
			_authorizationMiddleware = authorizationMiddleware;
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		#region Get Methods

		/// <summary>
		/// Get all orders
		/// </summary>
		/// <returns>Return an Array of all orders</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when there are no order in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Order>))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllOrders()
		{
			// Authorization Check
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");

			// Model State Check
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ORD-1101");

			// Retrieve orders from the repository
			var orders = await _orderRepository.GetAllOrders();
			if (orders.Count == 0)
				throw new NotFoundException("There is no order in the database, please try again", "ORD-1401");

			return Ok(orders);
		}

		/// <summary>
		/// Get a specific order
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific order</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when there are no order in the database.</exception>
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
				throw new BadRequestException("The model is wrong, a bad request occured", "ORD-1101");

			// Check if order exists
			if (!await _orderRepository.OrderExists(id))
				throw new NotFoundException("The order does with the id : " + id + " doesn't exist", "ORD-1401");

			// Retrieve order from the repository
			var order = await _orderRepository.GetOrderById(id);
			return Ok(order);
		}

		#endregion

		#region POST Method
		/// <summary>
		/// Create a new order
		/// </summary>
		/// <param name="order"></param>
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

		#region PUT Method

		/// <summary>
		/// Modify a specific order
		/// </summary>
		/// <param name="id"></param>
		/// <param name="order"></param>
		/// /// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid or if there is a missing field</exception>
		/// <exception cref="NotFoundException">Thrown when there are no order products associated with the user.</exception>
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
				throw new BadRequestException("The model is wrong, a bad request occured", "ORD-1101");

			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ORD-1102");

			// Check if order exists
			if (!await _orderRepository.OrderExists(id))
				throw new NotFoundException("The order does with the id : " + id + " doesn't exist", "ORD-1401");

			// Update order
			await _orderRepository.UpdateOrder(id, order);
			return Ok();
		}

		#endregion

		#region DELETE Method

		/// <summary>
		/// Delete a specific order
		/// </summary>
		/// <param name="id"></param>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized for this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the request model is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the order with the specified id doesn't exist.</exception>
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
				throw new BadRequestException("The model is wrong, a bad request occured", "ORD-1101");

			// Check if order exists
			if (!await _orderRepository.OrderExists(id))
				throw new NotFoundException("The order does with the id : " + id + " doesn't exist", "ORD-1401");

			// Delete order
			await _orderRepository.DeleteOrder(id);
			return Ok();
		}

		#endregion
	}
}
