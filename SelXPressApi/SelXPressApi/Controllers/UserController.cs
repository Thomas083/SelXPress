using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
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
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetUsers()
		{
            if (!ModelState.IsValid)
                throw new GetUsersBadRequestException("A bad request occured, please try again");

            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetAllUsers());

            if (users.Count == 0)
                throw new GetUsersNotFoundException("There are no users in the database");

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
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetUser(int id)
		{
            if (!await _userRepository.UserExists(id))
            {
                throw new GetUserByIdNotFoundException("The user with the id : " + id + "doesn't exist");
            }
            if (!ModelState.IsValid)
                throw new GetUserByIdBadRequestException("A bad request occured, please try again");

            var user = _mapper.Map<UserDto>(await _userRepository.GetUserById(id));
            return Ok(user);
        }

		/// <summary>
		/// POST api/<UserController>
		/// Create a new user
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		[ProducesResponseType(200, Type = typeof(User))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserDto newUser)
		{
            if (newUser.Username == null || newUser.Email == null || newUser.Password == null || newUser.RoleId == null
                    || newUser.RoleId == 0)
            {
                throw new CreateUserBadRequestException("There is a missing field, a bad request occured");
            }
			await _userRepository.CreateUser(newUser);
			return StatusCode(201);
        }

		/// <summary>
		/// PUT api/<UserController>/5
		/// Modify an user
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO userUpdate)
		{
            if (userUpdate == null)
                throw new UpdateUserBadRequestException("The value of the body is null, please try again with some data");

            if (!await _userRepository.UserExists(id))
                throw new UpdateUserNotFoundException("The user with the id " + id + " doesn't exist");

			await _userRepository.UpdateUser(userUpdate, id);
			return NoContent();


        }

		/// <summary>
		/// DELETE api/<UserController>/5
		/// Delete an user
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
            if (!await _userRepository.UserExists(id))
                throw new DeleteUserNotFoundException("The user with the id " + id + " doesn't exist");

			await _userRepository.DeleteUser(id);
			return Ok();
        }
	}
}
