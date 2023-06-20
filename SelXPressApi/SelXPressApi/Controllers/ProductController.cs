using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SelXPressApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Add a product to the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddProduct()
        {
            return Ok();
        }

        /// <summary>
        /// Modify the product in the database
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ModifyProduct()
        {
            return Ok();
        }

        /// <summary>
        /// Delete the product from the database
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteProduct()
        {
            return Ok();
        }

        /// <summary>
        /// Get all products from database
        /// </summary>
        /// <returns> all products</returns>
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok();
        }

        /// <summary>
        /// Get one product from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProductById(int id)
        {
            return Ok();
        }
    }
}
