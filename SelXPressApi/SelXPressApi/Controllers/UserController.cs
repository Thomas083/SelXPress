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
		[ProducesResponseType(400, Type = typeof(BadRequestErrorMessage))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorMessage))]
		[ProducesResponseType(500, Type = typeof(ServerErrorMessage))]
		public IActionResult GetUsers()
		{
			try
			{
                ICollection<User> result = _userRepository.GetAllUsers();

                if (!ModelState.IsValid)
                {
                    BadRequestErrorMessage error = new BadRequestErrorMessage("Bad request occured", 002);
                    return BadRequest(error);
                }

                if (result.Count == 0)
                {
                    NotFoundErrorMessage error = new NotFoundErrorMessage("There is no user in the database", 003);
                    return NotFound(error);
                }

                return Ok(result);
            }
			catch(Exception ex)
			{
				ServerErrorMessage error = new ServerErrorMessage(ex.Message,005);
				return StatusCode(500, error);
			}
		}

		/// <summary>
		/// GET api/<UserController>/5
		/// get user by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific user</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(User))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorMessage))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorMessage))]
		[ProducesResponseType(500, Type = typeof(ServerErrorMessage))]
		public IActionResult GetUser(int id)
		{
			try
			{
                if (!_userRepository.UserExists(id))
                {
                    NotFoundErrorMessage error = new NotFoundErrorMessage("There is no user with the id " + id, 001);
                    return NotFound(error);
                }

                if (!ModelState.IsValid)
                {
                    BadRequestErrorMessage error = new BadRequestErrorMessage("A badrequest occured", 004);
                    return BadRequest(error);
                }
                var user = _userRepository.GetUserById(id);
                return Ok(user);
            }
			catch(Exception ex)
			{
				ServerErrorMessage error = new ServerErrorMessage(ex.Message, 006);
				return StatusCode(500, error);
			}
		}

		/// <summary>
		/// POST api/<UserController>
		/// Create a new user
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void CreateUser([FromBody] string value)
		{

		}

		/// <summary>
		/// PUT api/<UserController>/5
		/// Modify an user
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void UpdateUser(int id, [FromBody] string value)
		{
		}

		/// <summary>
		/// DELETE api/<UserController>/5
		/// Delete an user
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void DeleteUser(int id)
		{
		}
	}
}
