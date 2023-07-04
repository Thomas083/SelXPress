using Microsoft.AspNetCore.Mvc;
using SelXPressApi.ErrorMessage;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		public UserController(IUserRepository userRepository)
		{
			this._userRepository = userRepository;
		}

		/// <summary>
		/// GET: api/<UserController>
		/// Get all users
		/// </summary>
		/// <returns>Return an Array of all user</returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(ICollection<User>))]
		[ProducesResponseType(400, Type = typeof(NotFoundErrorMessage))]
		public ActionResult<ICollection<User>> Get()
		{
			ICollection<User> result = _userRepository.GetAllUsers();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(result);
		}

		/// <summary>
		/// GET api/<UserController>/5
		/// get user by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific user</returns>
		[HttpGet("{id}")]
		public ActionResult Get(int id)
		{
			if (!_userRepository.UserExists(id))
			{
				return NotFound("The user with the id : " + id + " doesn't exists");
			}
			return Ok();


		}

		/// <summary>
		/// POST api/<UserController>
		/// Create a new user
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		/// <summary>
		/// PUT api/<UserController>/5
		/// Modify an user
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		/// <summary>
		/// DELETE api/<UserController>/5
		/// Delete an user
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
