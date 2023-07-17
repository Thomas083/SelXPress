using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Exceptions.Product;
using SelXPressApi.Interfaces;

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
		[ProducesResponseType(200, Type = typeof(ICollection<ProductDTO>))]
		[ProducesResponseType(400, Type = typeof(GetProductBadRequestException))]
		[ProducesResponseType(404, Type = typeof(GetProductNotFoundException))]
		public async Task<ActionResult> GetProducts()
		{
			try
			{
				if(!ModelState.IsValid)
					throw new GetProductBadRequestException("A bad request occured to return the", "1000", 400);

				var products = _mapper .Map<List<ProductDTO>>(_productRepository.GetAllProducts());

				if(products.Count == null)
					throw new GetProductNotFoundException("There is no product in the database, please create some", "1001", 404);
				return Ok(products);
			}
			catch(GetProductBadRequestException ex)
			{
				return BadRequest(ex);
			}
			catch (GetProductNotFoundException ex)
			{
				return NotFound(ex);
			}
		}

		/// <summary>
		/// GET api/<ProductController>/5
		/// Get a specific product
		/// </summary>
		/// <param name="id"></param>
		/// <returns>information of a specific product</returns>
		[HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ICollection<ProductDTO>))]
        [ProducesResponseType(400, Type = typeof(GetProductBadRequestException))]
        [ProducesResponseType(404, Type = typeof(GetProductNotFoundException))]
        public async Task<ActionResult> GetProductByID(int id)
		{
            try
            {
                if (!ModelState.IsValid)
                    throw new GetProductBadRequestException("A bad request occured to return the", "1000", 400);

				if (!_productRepository.ProductExists(id))
					throw new GetProductNotFoundException("product not found", "1001", 404);
				var product = _productRepository.GetProductById(id);
                return Ok(product);
            }
            catch (GetProductBadRequestException ex)
            {
                return BadRequest(ex);
            }
            catch (GetProductNotFoundException ex)
            {
                return NotFound(ex);
            }
        }

		/// <summary>
		/// POST api/<ProductController>
		/// Create a new product
		/// </summary>
		/// <param name="value"></param>
		[HttpPost("addProduct")]
		public void Post([FromBody] string value)
		{

		}

		/// <summary>
		/// PUT api/<ProductController>/5
		/// Modify a product
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("updateProduct/{id}")]
		public void Put(int id, [FromBody] string value)
		{

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
