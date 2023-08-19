using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;
		public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, IAuthorizationMiddleware authorizationMiddleware)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
			_authorizationMiddleware = authorizationMiddleware;
		}
		
		/// <summary>
		/// Method to get all the categories of the database
		/// </summary>
		/// <returns></returns>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CategoryDTO>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetCategory()
		{
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "CAT-1101");

            var categories = _mapper.Map<List<CategoryDTO>>(await _categoryRepository.GetAllCategories());

            if (categories.Count == 0)
                throw new NotFoundException("There is no categories in the database, please try again", "CAT-1401");

            return Ok(categories);
        }

		/// <summary>
		/// Method to get the category based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CategoryDTO))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetCategory(int id)
		{
			if(!await _categoryRepository.CategoryExists(id))
				throw new NotFoundException("The category with the id : " + id + " doesn't exist", "CAT-1402");
			if(!ModelState.IsValid)
				throw new BadRequestException("The model is wrong , a bad request occured", "CAT-1101");

			var category = _mapper.Map<CategoryDTO>(await _categoryRepository.GetCategoryById(id));
			return Ok(category);
        }

		/// <summary>
		/// Method to create a new category
		/// </summary>
		/// <param name="newCategory"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO newCategory)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "CAT-2001");
			
			if(newCategory.Name == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "CAT-11102");
            
			await _categoryRepository.CreateCategory(newCategory);
			return StatusCode(201);
		}

		/// <summary>
		/// Method to update a category based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="categoryUpdate"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTO categoryUpdate)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "CAT-2001");
			
			if(categoryUpdate == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "CAT-1102");
			
			if(!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CAT-1101");
			
			if(!await _categoryRepository.CategoryExists(id))
                throw new NotFoundException("The category with the id : " + id + " doesn't exist", "CAT-1402");
            
			await _categoryRepository.UpdateCategory(categoryUpdate, id);
			return Ok();
		}

		/// <summary>
		/// Method to delete a cateogry based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteCategory(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "CAT-2001");
			
			if (!await _categoryRepository.CategoryExists(id))
				throw new NotFoundException("The category with the id :" + id + " doesn't exist", "CAT-1402");
			
			if(!ModelState.IsValid)
                throw new BadRequestException("The model is wrong , a bad request occured", "CAT-1101");
            
			await _categoryRepository.DeleteCategory(id);
			return Ok();
		}
	}
}
