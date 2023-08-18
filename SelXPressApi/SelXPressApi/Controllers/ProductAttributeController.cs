using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.Exceptions;
using SelXPressApi.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductAttributeController : ControllerBase
	{
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		public ProductAttributeController(IAuthorizationMiddleware authorizationMiddleware)
		{
			_authorizationMiddleware = authorizationMiddleware;
		}
		/// <summary>
		/// GET: api/<ProductAttributeController>
		/// Get all product attributes
		/// </summary>
		/// <returns>Return an Array of all product's attribute</returns>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetProductAttributes()
		{
			//todo put the code logic after this
			return Ok();
		}

		/// <summary>
		/// GET api/<ProductAttributeController>/5
		/// Get a product attribute by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific attribute of the product</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> Get(int id)
		{
			//todo put the code logic after this
			return Ok();
		}

		/// <summary>
		/// POST api/<ProductAttributeController>
		/// Create a new product attribute
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateProductAttribute([FromBody] string value)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "PAT-2001");
			//todo put the code logic after this and set the parameter
			return StatusCode(201);
		}

		/// <summary>
		/// PUT api/<ProductAttributeController>/5
		/// Modify a product attribute
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateProductAttribute(int id, [FromBody] string value)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "PAT-2001");
			//todo put the code logic after this and set the parameters
			return Ok();
		}

		/// <summary>
		/// DELETE api/<ProductAttributeController>/5
		/// Delete a product attribute
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteProductAttribute(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "PAT-2001");
			//todo put the code logic after this and set the parameter
			return Ok();
		}
	}
}
