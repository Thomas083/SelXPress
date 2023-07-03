using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		/// <summary>
		/// GET: api/<UserController>
		/// Get all users
		/// </summary>
		/// <returns>array of all user</returns>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		/// <summary>
		/// GET api/<UserController>/5
		/// get user by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>an user</returns>
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		/// <summary>
		/// POST api/<UserController>
		/// create a new user
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		/// <summary>
		/// PUT api/<UserController>/5
		/// modify an user
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		/// <summary>
		/// DELETE api/<UserController>/5
		/// delete an user
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
