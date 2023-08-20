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
		/// <summary>
		/// Get all orders
		/// </summary>
		/// <returns>Return an Array of all orders</returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Order>))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllOrders()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");
			
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
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Order))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrder(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ORD-1101");

			if (!await _orderRepository.OrderExists(id))
				throw new NotFoundException("The order does with the id : " + id + " doesn't exist", "ORD-1401");

			var order = await _orderRepository.GetOrderById(id);
			return Ok(order);
		}

		/// <summary>
		/// Create a new order
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		[ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO order)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");
			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ORD-1102"); ;

			await _orderRepository.CreateOrder(order);
			return StatusCode(201);
		}

		/// <summary>
		/// Modify a specific order
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDTO order)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ORD-1101");

			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ORD-1102"); ;

			if (!await _orderRepository.OrderExists(id))
				throw new NotFoundException("The order does with the id : " + id + " doesn't exist", "ORD-1401");

			await _orderRepository.UpdateOrder(id, order);
			return Ok();
		}

		/// <summary>
		/// Delete a specific order
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ORD-2001");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ORD-1101");

			if (!await _orderRepository.OrderExists(id))
				throw new NotFoundException("The order does with the id : " + id + " doesn't exist", "ORD-1401");

			await _orderRepository.DeleteOrder(id);
			return Ok();
		}
	}
}
