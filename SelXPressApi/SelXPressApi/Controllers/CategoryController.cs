using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Models;

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing Categories. 
	/// Here you can access to DTO <see cref="CategoryDTO"/>. 
	/// The model <see cref="Models.Cart"/>
	/// </summary>
	/// <seealso  cref="Models"/>
	/// <seealso  cref="DTO"/>
	/// <seealso  cref="Controllers"/>
	/// <seealso  cref="Repository"/>
	/// <seealso  cref="Helper"/>
	/// <seealso  cref="DocumentationErrorTemplate"/>
	/// <seealso  cref="Exceptions"/>
	/// <seealso  cref="Interfaces"/>
	/// <seealso  cref="Middleware"/>
	/// <seealso  cref="Data"/>

	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryController"/> class.
		/// </summary>
		/// <param name="categoryRepository">The category repository to retrieve and manage category. <see cref="ICategoryRepository"/></param>
		/// <param name="mapper">The AutoMapper instance for object mapping. <see cref="IMapper"/></param>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations. <see cref="IAuthorizationMiddleware"/></param>
		public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, IAuthorizationMiddleware authorizationMiddleware)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
			_authorizationMiddleware = authorizationMiddleware;
		}

		#region Get Methods
		/// <summary>
		/// Get all categories from the database.
		/// </summary>
		/// <returns>Returns a list of all categories.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when no categories are found in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<CategoryDTO>))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCategories()
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CAT-1101");

			// Retrieve all categories from the repository and map them to CategoryDTO
			var categories = _mapper.Map<List<CategoryDTO>>(await _categoryRepository.GetAllCategories());

			// Check if any categories were retrieved
			if (categories.Count == 0)
				throw new NotFoundException("There are no categories in the database, please try again", "CAT-1401");

			// Return the list of categories
			return Ok(categories);
		}

		/// <summary>
		/// Get a category based on its ID.
		/// </summary>
		/// <param name="id">The ID of the category to retrieve.</param>
		/// <returns>Returns the category with the specified ID.</returns>
		/// <exception cref="NotFoundException">Thrown when the category with the specified ID is not found.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(CategoryDTO))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetCategory(int id)
		{
			// Check if the category with the given ID exists
			if (!await _categoryRepository.CategoryExists(id))
				throw new NotFoundException($"The category with the ID : {id} doesn't exist", "CAT-1402");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CAT-1101");

			// Retrieve the category by its ID
			var category = _mapper.Map<CategoryDTO>(await _categoryRepository.GetCategoryById(id));
			return Ok(category);
		}
		#endregion

		#region Post Method
		/// <summary>
		/// Create a new category.
		/// </summary>
		/// <param name="newCategory">The data for creating the new category.</param>
		/// <returns>Returns 201 Created if the category is successfully created.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or required fields are missing.</exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO newCategory)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "CAT-2001");

			// Check if the provided category data is complete
			if (newCategory.Name == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "CAT-1102");

			// Create the attribute using the repository
			await _categoryRepository.CreateCategory(newCategory);
			return StatusCode(201);
		}
		#endregion

		#region Put Method
		/// <summary>
		/// Update a category based on its ID.
		/// </summary>
		/// <param name="id">The ID of the category to update.</param>
		/// <param name="categoryUpdate">The updated category information.</param>
		/// <returns>Returns a successful response if the category is updated successfully.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or required fields are missing.</exception>
		/// <exception cref="NotFoundException">Thrown when the category with the specified ID is not found.</exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTO categoryUpdate)
		{
			// Check if the user's token exists
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "CAT-2001");

			// Check if the category update data is missing
			if (categoryUpdate == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "CAT-1102");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CAT-1101");

			// Check if the category with the specified ID exists
			if (!await _categoryRepository.CategoryExists(id))
				throw new NotFoundException($"The category with the ID : { id } doesn't exist", "CAT-1402");

            // Update the category
            await _categoryRepository.UpdateCategory(categoryUpdate, id);

			return Ok();
		}
		#endregion

		#region Delete Method
		/// <summary>
		/// Delete a category based on its ID.
		/// </summary>
		/// <param name="id">The ID of the category to delete.</param>
		/// <returns>Returns a successful response if the category is deleted successfully.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="NotFoundException">Thrown when the category with the specified ID is not found.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			// Check if the user's token exists
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "CAT-2001");

			// Check if the category with the specified ID exists
			if (!await _categoryRepository.CategoryExists(id))
				throw new NotFoundException($"The category with the ID : {id} doesn't exist", "CAT-1402");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "CAT-1101");

			// Delete the category
			await _categoryRepository.DeleteCategory(id);

			return Ok();
		}
		#endregion
	}
}
