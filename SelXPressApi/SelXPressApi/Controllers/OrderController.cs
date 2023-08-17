using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.Exceptions;
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
		public OrderController(IAuthorizationMiddleware authorizationMiddleware)
		{
			_authorizationMiddleware = authorizationMiddleware;
		}
		/// <summary>
		/// GET: api/<OrderController>
		/// Get all orders
		/// </summary>
		/// <returns>Return an Array of all orders</returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Order>))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrders()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this
			
			return Ok();
		}

		/// <summary>
		/// GET api/<OrderController>/5
		/// Get a specific order
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific order</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Order))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrder(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this
			return Ok();
		}

		/// <summary>
		/// POST api/<OrderController>
		/// Create a new order
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateOrder([FromBody] string value)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo  put the code login after this and set the parameter
			return StatusCode(201);
		}

		/// <summary>
		/// PUT api/<OrderController>/5
		/// Modify a specific order
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateOrder(int id, [FromBody] string value)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this an set the parameters
			return Ok();
		}

		/// <summary>
		/// DELETE api/<OrderController>/5
		/// Delete a specific order
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this
			return Ok();
		}
	}
}
