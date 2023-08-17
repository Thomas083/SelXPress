using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.Exceptions;
using SelXPressApi.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// Crud operations for products
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		public ProductController(IAuthorizationMiddleware authorizationMiddleware)
		{
			_authorizationMiddleware = authorizationMiddleware;
		}
		/// <summary>
		/// GET: api/<ProductController>
		/// Get all products
		/// </summary>
		/// <returns>Return an Array of all product</returns>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetProducts()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this
			return Ok();
		}

		/// <summary>
		/// GET api/<ProductController>/5
		/// Get a specific product
		/// </summary>
		/// <param name="id"></param>
		/// <returns>information of a specific product</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetProduct(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this and set the parameter
			return Ok();
		}

		/// <summary>
		/// Methode qui retourne tous les produits appertennant à un utilisateur (méthode pour les sellers)
		/// Retourner les produits avec l'adresse mail de l'utilisateur qui les a créé
		/// </summary>
		/// <returns></returns>
		[HttpGet("me")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetProductsBySeller()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			string? email = HttpContext.Request.Headers["EmailHeader"];
			//todo put the code logic after this
			return Ok();
		}
		
		
		/// <summary>
		/// Methode qui retourne le produit par rapport à son id appertennant à un utilisateur (méthode pour les sellers)
		/// Retourner le produit avec l'adresse mail de l'utilisateur qui les a créé et l'id du produit
		/// </summary>
		/// <returns></returns>
		[HttpGet("me/{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetProductBySeller(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			string? email = HttpContext.Request.Headers["EmailHeader"];
			//todo put the code logic after this
			return Ok();
		}

		/// <summary>
		/// POST api/<ProductController>
		/// Create a new product
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateProduct([FromBody] string value)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this and set the parameter
			return StatusCode(201);
		}

		/// <summary>
		/// PUT api/<ProductController>/5
		/// Modify a product
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody] string value)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this and set the parameters
			return Ok();
		}

		/// <summary>
		/// DELETE api/<ProductController>/5
		/// Delete a product
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("deleteProduct/{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			//todo put the code logic after this and set the parameter
			return Ok();
		}
	}
}
