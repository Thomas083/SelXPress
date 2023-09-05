using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using System.Collections.Generic;
using System.Threading.Tasks;
using SelXPressApi.DTO.OrderDTOProductDTO;

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing order product.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class OrderProductController : ControllerBase
	{
		private readonly IAuthorizationMiddleware _authorizationMiddleware;
		private readonly IOrderProductRepository _orderProductRepository;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderProductController"/> class.
		/// </summary>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		/// <param name="orderProductRepository">The order product repository to retrieve and manage order product</param>
		/// <param name="mapper">The AutoMapper instance for object mapping.</param>
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
			// Check if the token exists in the HttpContext for authorization.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the necessary role (in this case, admin) for the operation.
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "ODP-2001");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ODP-1101");

			// Retrieve all order products from the repository.
			var orderProducts = await _orderProductRepository.GetAllOrderProducts();

			// If there are no order products in the database, throw a NotFoundException.
			if (orderProducts.Count == 0)
				throw new NotFoundException("There are no order products found in the database, please try again", "ODP-1401");

			// Return the retrieved order products as an Ok response.
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
			// Check if the token exists in the HttpContext for authorization.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the necessary role (in this case, customer) for the operation.
			if (!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "ODP-2001");

			// Get the email of the user from the response headers.
			string? email = HttpContext.Response.Headers["EmailHeader"];

			// If the email is missing or empty, throw a BadRequestException.
			if (string.IsNullOrEmpty(email))
				throw new BadRequestException("The email is missing in the request header", "ODP-1104");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ODP-1101");

			// Retrieve order products associated with the specified user's email.
			var orderProducts = await _orderProductRepository.GetOrderProductsByUser(email);

			// If no order products are found for the user, throw a NotFoundException.
			if (orderProducts.Count == 0)
				throw new NotFoundException("There are no order products found in the database, please try again", "ODP-1401");

			// Return the retrieved order products as an Ok response.
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
			// Check if the token exists in the HttpContext for authorization.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the necessary role (in this case, admin) for the operation.
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "ODP-2001");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ODP-1101");

			// Retrieve the order product by its ID.
			var orderProduct = await _orderProductRepository.GetOrderProductById(id);

			// If the order product is not found, throw a NotFoundException.
			if (orderProduct == null)
				throw new NotFoundException("There are no order products found in the database, please try again", "ODP-1401");

			// Return the retrieved order product as an Ok response.
			return Ok(orderProduct);
		}
		#endregion

		#region Post Method
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
			// Check if the token exists in the HttpContext for authorization.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the necessary role (admin or customer) for the operation.
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "ODP-2001");

			// Check if the order data is provided.
			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ODP-1102");

			// Create the order product using the provided order data.
			await _orderProductRepository.CreateOrderProduct(order);

			// Return a response with a status code indicating successful creation (201 Created).
			return StatusCode(201);
		}
		#endregion

		#region Put Method
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
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateOrderProduct(int id, [FromBody] UpdateOrderProductDTO order)
		{
			// Check if the token exists in the HttpContext for authorization.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the admin role for this operation.
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "ODP-2001");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ODP-1101");

			// Check if the order data is provided.
			if (order == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ODP-1102");

			// Check if the order product with the given id exists.
			if (!await _orderProductRepository.OrderProductExists(id))
				throw new NotFoundException($"The order product with the ID : {id} doesn't exist", "ODP-1402");

			// Update the order product with the provided data.
			await _orderProductRepository.UpdateOrderProduct(id, order);

			// Return a response indicating successful update.
			return Ok();
		}

		#endregion

		#region DeleteMethod
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
				throw new NotFoundException($"The order product with the ID : {id} doesn't exist", "ODP-1402");

			await _orderProductRepository.DeleteOrderProduct(id);
			return Ok();
		}
		#endregion
	}
}
