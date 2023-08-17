using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.Exceptions;
using SelXPressApi.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderProductController : ControllerBase
	{
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		public OrderProductController(IAuthorizationMiddleware authorizationMiddleware)
		{
			_authorizationMiddleware = authorizationMiddleware;
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
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrderProducts()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this
			return Ok();
		}

		/// <summary>
		/// Methode qui va récupérer toutes les commandes et produits associés de l'utilisateur courant
		/// il faut récupérer les orderProduct en fonction de l'adresse mail contenu dans un header
		/// </summary>
		/// <returns></returns>
		[HttpGet("me")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrderProductByUser()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			string? email = HttpContext.Request.Headers["EmailHeader"]; //email of the user
			//todo put the code logic after this
			return Ok();
		}

		/// <summary>
		/// GET api/<OrderProductController>/5
		/// Get a specific order product
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific order product</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetOrderProduct(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this
			return Ok();
		}

		/// <summary>
		/// POST api/<OrderProductController>
		/// Create a new order product
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateOrderProduct([FromBody] string value)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this and set the parameter
			return StatusCode(201);
		}

		/// <summary>
		/// PUT api/<OrderProductController>/5
		/// Modify a specific order product
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateOrderProduct(int id, [FromBody] string value)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this and set the parameters 
			return Ok();
		}

		/// <summary>
		/// DELETE api/<OrderProductController>/5
		/// Delete a specific order product
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteOrderProduct(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this and set the parameter
			return Ok();
		}
	}
}
