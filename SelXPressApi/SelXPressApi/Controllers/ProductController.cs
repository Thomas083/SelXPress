using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Exceptions.Product;
using SelXPressApi.Exceptions.User;
using SelXPressApi.Interfaces;
using System.Runtime.InteropServices;

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
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
		public ProductController(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}
		/// <summary>
		/// GET: api/<ProductController>
		/// Get all products
		/// </summary>
		/// <returns>Return an Array of all product</returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<ProductDTO>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<ActionResult> GetProducts()
		{
			if(!ModelState.IsValid)
				throw new GetProductsBadRequestException("The model is wrong, a bad request occured");


            var products = _mapper .Map<List<ProductDTO>>(_productRepository.GetAllProducts());

			if(products.Count == 0)
				throw new GetProductsNotFoundException("There are no products in the database");
			return Ok(products);
		}

		/// <summary>
		/// GET api/<ProductController>/5
		/// Get a specific product
		/// </summary>
		/// <param name="id"></param>
		/// <returns>information of a specific product</returns>
		[HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ICollection<ProductDTO>))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<ActionResult> GetProductByID(int id)
		{
            if (!await _productRepository.ProductExists(id))
				throw new GetProductByIdNotFoundException("The user with the id : " + id + "doesn't exist");
            if (!ModelState.IsValid)
                throw new GetProductByIdBadRequestException("The model is wrong, a bad request occured");

            var product = _productRepository.GetProductById(id);
            return Ok(product);
        }

		/// <summary>
		/// POST api/<ProductController>
		/// Create a new product
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO newProduct)
		{
			if (newProduct.Name == null || newProduct.Description == null || newProduct.Picture == null || newProduct.Price == null || newProduct.ProductAttributes == null || newProduct.Category == null)
				throw new CreateProductBadRequestException("There is a missing field, a bad request occured");

			if (await _productRepository.CreateProduct(newProduct))
				return StatusCode(201);
			throw new Exception("An error occured while the creation of the user");
		}

		/// <summary>
		/// PUT api/<ProductController>/5
		/// Modify a product
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO productUpdate)
		{
			if (productUpdate == null)
				throw new UpdateProductBadRequestException("The value of the body is null, please try again with some data");
			if(!ModelState.IsValid)
				throw new UpdateProductBadRequestException("The model is not valid, a bad request occured");
			if (!await _productRepository.UpdateProduct(productUpdate, id))
				return Ok();
            throw new Exception("An error occured while the update of the user");

        }

        /// <summary>
        /// DELETE api/<ProductController>/5
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("deleteProduct/{id}")]
		public void Delete(int id)
		{
		}
	}
}
