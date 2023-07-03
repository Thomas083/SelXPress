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
		/// </summary>
		/// <returns>Array of all cart</returns>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		/// <summary>
		/// GET api/<CartController>/5
		/// </summary>
		/// <param name="id"></param>
		/// <returns>information of the cart</returns>
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		/// <summary>
		/// POST api/<CartController>
		/// create new cart
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void Post([FromBody] string value)
		{

		}

		/// <summary>
		/// PUT api/<CartController>/5
		/// modify a cart
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}
		/// <summary>
		/// DELETE api/<CartController>/5
		/// delete a cart
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
