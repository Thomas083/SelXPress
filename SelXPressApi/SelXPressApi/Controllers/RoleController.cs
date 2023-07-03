using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		/// <summary>
		/// GET: api/<RoleController>
		/// Get all roles
		/// </summary>
		/// <returns>Return an Array with all roles </returns>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		/// <summary>
		/// GET api/<RoleController>/5
		/// Get a role by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a list of user with a specific role</returns>
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		/// <summary>
		/// POST api/<RoleController>
		/// Create a new role
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		/// <summary>
		/// PUT api/<RoleController>/5
		/// Modify a role
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		/// <summary>
		/// DELETE api/<RoleController>/5
		/// Delete a role
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
