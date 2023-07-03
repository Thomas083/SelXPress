using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		/// <summary>
		/// GET: api/<CartController>
		/// Get all carts
		/// </summary>
		/// <returns>Return an Array of all cart</returns>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		/// <summary>
		/// GET api/<CartController>/5
		/// Get a cart by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return the information of a specific cart</returns>
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		/// <summary>
		/// POST api/<CartController>
		/// Create new cart
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void Post([FromBody] string value)
		{

		}

		/// <summary>
		/// PUT api/<CartController>/5
		/// Modify a cart
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}
		/// <summary>
		/// DELETE api/<CartController>/5
		/// Delete a cart
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
