using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductAttributeController : ControllerBase
	{
		/// <summary>
		/// GET: api/<ProductAttributeController>
		/// Get all product attributes
		/// </summary>
		/// <returns>Return an Array of all product's attribute</returns>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		/// <summary>
		/// GET api/<ProductAttributeController>/5
		/// Get a product attribute by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific attribute of the product</returns>
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		/// <summary>
		/// POST api/<ProductAttributeController>
		/// Create a new product attribute
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		/// <summary>
		/// PUT api/<ProductAttributeController>/5
		/// Modify a product attribute
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		/// <summary>
		/// DELETE api/<ProductAttributeController>/5
		/// Delete a product attribute
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
