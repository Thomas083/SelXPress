using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;
		public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}
		/// <summary>
		/// GET: api/<CategoryController>
		/// Get all categories
		/// </summary>
		/// <returns>Return an Array of all categories</returns>
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
		/// GET api/<CategoryController>/5
		/// Get a category by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific category</returns>
		[HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(List<CategoryDTO>))]
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
		/// POST api/<CategoryController>
		/// Create a new category
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO newCategory)
		{
			if(newCategory.Name == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "CAT-11102");

			if (await _categoryRepository.CreateCategory(newCategory))
				return StatusCode(201);
            throw new Exception("An error occured while the creation of the user");
        }

		/// <summary>
		/// PUT api/<CategoryController>/5
		/// Modify a category
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTO categoryUpdate)
		{
			if(categoryUpdate == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "CAT-1102");
			if(!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "CAT-1101");
			if(!await _categoryRepository.CategoryExists(id))
                throw new NotFoundException("The category with the id : " + id + " doesn't exist", "CAT-1402");
			if (!await _categoryRepository.UpdateCategory(categoryUpdate, id))
				return Ok();
            throw new Exception("An error occured while the update of the categorie");
        }

		/// <summary>
		/// DELETE api/<CategoryController>/5
		/// Delete a category
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteCategory(int id)
		{
			if (!await _categoryRepository.CategoryExists(id))
				throw new NotFoundException("The category with the id :" + id + " doesn't exist", "CAT-1402");
			if(!ModelState.IsValid)
                throw new BadRequestException("The model is wrong , a bad request occured", "CAT-1101");
			if(!await _categoryRepository.DeleteCategory(id))
				return Ok();
            throw new Exception("An error occured while the deleting of the user");
        }
	}
}
