using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Exceptions.User;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		public UserController(IUserRepository userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		/// <summary>
		/// GET method to get all the users
		/// </summary>
		/// <returns>List of UserD</returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<UserDto>))]
		//[ProducesResponseType(400, Type = typeof(GetUsersBadRequestException))]
		//[ProducesResponseType(404, Type = typeof(GetUsersNotFoundException))]
		//[ProducesResponseType(500, Type = typeof(Exception))]
		public async Task<IActionResult> GetUsers()
		{
            if (!ModelState.IsValid)
                throw new GetUsersBadRequestException("test bad request", "test bad request");

            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetAllUsers());

            if (users.Count == 0)
                throw new GetUsersNotFoundException("test not found", "test not found");

            return Ok(users);
        }

		/// <summary>
		/// GET api/<UserController>/5
		/// get user by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific user</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(UserDto))]
		//[ProducesResponseType(400, Type = typeof(GetUserByIdBadRequestException))]
		//[ProducesResponseType(404, Type = typeof(GetUserByIdNotFoundException))]
		//[ProducesResponseType(500, Type = typeof(Exception))]
		public async Task<ActionResult> GetUser(int id)
		{
			try
			{
				if(!await _userRepository.UserExists(id))
				{
					throw new GetUserByIdNotFoundException("The user with the id : " + id + "doesn't exist", "USR-1003", 404);
				}
				if (!ModelState.IsValid)
					throw new GetUserByIdBadRequestException("A bad request occured, please try again", "USR-1004", 400);

                var user = _mapper.Map<UserDto>(await _userRepository.GetUserById(id));
                return Ok(user);
            }
			catch(GetUserByIdNotFoundException ex)
			{
				return NotFound(ex);
			}
			catch(GetUserByIdBadRequestException ex)
			{
				return BadRequest(ex);
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		/// <summary>
		/// POST api/<UserController>
		/// Create a new user
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public async Task<ActionResult> CreateUser([FromBody] CreateUserDto newUser)
		{
			try
			{
				if(newUser.Username == null || newUser.Email == null || newUser.Password == null || newUser.RoleId == null
					|| newUser.RoleId == 0)
				{
					throw new CreateUserBadRequestException("There is a missing field, a bad request occured", "USR-1005", 400);
				}
				return Created("api/UserController/",typeof(User));
			}
			catch(CreateUserBadRequestException ex)
			{
				return BadRequest(ex);
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		/// <summary>
		/// PUT api/<UserController>/5
		/// Modify an user
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO userUpdate)
		{
			try
			{
				if (userUpdate == null)
					throw new UpdateUserBadRequestException("The value of the body is null, please try again with some data", "USR-1006", 400);

				if (!await _userRepository.UserExists(id))
					throw new UpdateUserNotFoundException("The user with the id " + id + " doesn't exist", "USR-1007", 404);
				
				if(await _userRepository.UpdateUser(userUpdate, id))
				{
					return NoContent();
				}
				else
				{
					throw new UpdateUserBadRequestException("A bad request occured because the data doesn't correspond at what is expected", "USR-1008", 400);
				}
			}
			catch(UpdateUserBadRequestException ex)
			{
				return BadRequest(ex);
			}
			catch(UpdateUserNotFoundException ex)
			{
				return NotFound(ex);
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex);
			}
		}

		/// <summary>
		/// DELETE api/<UserController>/5
		/// Delete an user
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteUser(int id)
		{
			try
			{
				if (!await _userRepository.UserExists(id))
					throw new DeleteUserNotFoundException("The user with the id " + id + " doesn't exist", "USR-1009", 404);

				if(await _userRepository.DeleteUser(id))
				{
					return NoContent();
				}
				else
				{
					throw new DeleteUserBadRequestException("A bad request occured when deleting the user", "USR-1010", 401);
				}
			}
			catch(DeleteUserNotFoundException ex)
			{
				return NotFound(ex);
			}
			catch(DeleteUserBadRequestException ex)
			{
				return BadRequest(ex);
			}
			catch(Exception ex)
			{
				return StatusCode(500, ex);
			}
		}
	}
}
