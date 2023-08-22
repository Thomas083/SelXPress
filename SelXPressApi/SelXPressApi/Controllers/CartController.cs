using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.CartDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// Controller for managing shopping carts.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartRepository _cartRepository;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		/// <summary>
		/// Initializes a new instance of the <see cref="CartController"/> class.
		/// </summary>
		/// <param name="cartRepository">The repository for managing shopping carts.</param>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		public CartController(ICartRepository cartRepository, IAuthorizationMiddleware authorizationMiddleware)
		{
			_cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
			_authorizationMiddleware = authorizationMiddleware ?? throw new ArgumentNullException(nameof(authorizationMiddleware));
		}

		#region Get Methods

		/// <summary>
		/// Get all the shopping carts from the database.
		/// </summary>
		/// <returns>Returns an array of all shopping carts.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when no shopping carts are found in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Cart>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllCarts()
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "CRT-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CRT-1101");

			// Retrieve all carts from the repository
			var carts = await _cartRepository.GetAllCarts();

			// Check if any carts were found
			if (carts.Count == 0)
				throw new NotFoundException("There are no shopping carts in the database, please try again", "CRT-1401");

			return Ok(carts);
		}

		/// <summary>
		/// Get a shopping cart based on the id.
		/// </summary>
		/// <param name="id">The id of the shopping cart to retrieve.</param>
		/// <returns>Returns the shopping cart with the specified id.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the shopping cart with the specified id is not found.</exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Cart))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCartById(int id)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "CRT-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CRT-1101");

			// Check if the cart with the given ID exists
			if (!await _cartRepository.CartExists(id))
				throw new NotFoundException("The shopping cart with the id : " + id + " doesn't exist", "CRT-1402");

			// Retrieve the cart by its ID
			var cart = await _cartRepository.GetCartById(id);
			return Ok(cart);
		}

		/// <summary>
		/// Get all the shopping carts based on the user id.
		/// </summary>
		/// <param name="id">The id of the user.</param>
		/// <returns>Returns an array of shopping carts associated with the user id.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when no shopping carts are found for the user id.</exception>
		[HttpGet("{id}/user")]
		[ProducesResponseType(200, Type = typeof(List<Cart>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCartByUserId(int id)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "CRT-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CRT-1101");

			// Retrieve the attribute by its ID
			var carts = await _cartRepository.GetCartsByUserId(id);

			// Check if any cart were found
			if (carts.Count == 0)
				throw new NotFoundException("There are no shopping carts for the user with the id : " + id, "CRT-1403");

			return Ok(carts);
		}

		#endregion

		#region Create Methods

		/// <summary>
		/// Create a shopping cart by admin.
		/// </summary>
		/// <param name="cartDto">The data for creating the shopping cart.</param>
		/// <returns>Returns 200 OK if the shopping cart is successfully created.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or required fields are missing.</exception>
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateCartByAdmin([FromBody] CreateCartByAdminDto cartDto)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "CRT-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CRT-1101");

			// Check if the provided cart data is complete
			if (cartDto == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "CRT-1102");

			await _cartRepository.CreateCartByAdmin(cartDto);
			return Ok();
		}

		/// <summary>
		/// Creates a shopping cart for a user.
		/// </summary>
		/// <param name="cartDto">The data to create the shopping cart.</param>
		/// <returns>Returns 200 OK if the shopping cart is successfully created.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or required fields are missing.</exception>
		[HttpPost("me")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateCartByUser([FromBody] CreateCartDto cartDto)
		{
			// Check if a valid authentication token exists in the current HTTP context.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the necessary role to perform this operation (admin or customer).
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) && !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "CRT-2001");

			// Retrieve the user's email from the response headers.
			string? email = HttpContext.Response.Headers["EmailHeader"];

			// Check if the model state is valid (data annotations on the DTO).
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CRT-1101");

			// Check if the provided cart data (DTO) is null.
			if (cartDto == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "CRT-1102");

			// Create a new shopping cart for the user using the repository.
			await _cartRepository.CreateCartByUser(cartDto, email);

			// Return a 200 OK response indicating the successful creation of the shopping cart.
			return Ok();
		}

		#endregion

		#region Update Methods

		/// <summary>
		/// Updates a shopping cart based on its ID.
		/// </summary>
		/// <param name="id">The ID of the shopping cart to update.</param>
		/// <param name="cartDto">The data to update the shopping cart.</param>
		/// <returns>Returns 200 OK if the shopping cart is successfully updated.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="NotFoundException">Thrown when the shopping cart with the specified ID is not found.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or required fields are missing.</exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateCart(int id, [FromBody] UpdateCartDto cartDto)
		{
			// Check if a valid authentication token exists in the current HTTP context.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the necessary role to perform this operation (admin).
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "CRT-2001");

			// Check if a shopping cart with the provided ID exists in the database.
			if (!await _cartRepository.CartExists(id))
				throw new NotFoundException("The shopping cart with the id : " + id + " doesn't exist", "CRT-1402");

			// Check if the model state is valid (data annotations on the DTO).
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CRT-1101");

			// Check if the provided cart data (DTO) is null.
			if (cartDto == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "CRT-1102");

			// Update the shopping cart with the provided data using the repository.
			await _cartRepository.UpdateCart(cartDto, id);

			// Return a 200 OK response indicating the successful update of the shopping cart.
			return Ok();
		}


		#endregion

		#region Delete Methods

		/// <summary>
		/// Deletes a shopping cart based on its ID.
		/// </summary>
		/// <param name="id">The ID of the shopping cart to delete.</param>
		/// <returns>Returns 200 OK if the shopping cart is successfully deleted.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="NotFoundException">Thrown when the shopping cart with the specified ID is not found.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteCart(int id)
		{
			// Check if a valid authentication token exists in the current HTTP context.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the necessary roles to perform this operation (admin or customer).
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "CRT-2001");

			// Check if the model state is valid (data annotations on the DTO).
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CRT-1101");

			// Check if a shopping cart with the provided ID exists in the database.
			if (!await _cartRepository.CartExists(id))
				throw new NotFoundException("The shopping cart with the id : " + id + " doesn't exist", "CRT-1402");

			// Delete the shopping cart with the provided ID using the repository.
			await _cartRepository.DeleteCart(id);

			// Return a 200 OK response indicating the successful deletion of the shopping cart.
			return Ok();
		}


		#endregion
	}
}
