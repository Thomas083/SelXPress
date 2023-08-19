using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.MarkDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MarkController : ControllerBase
	{
		private readonly IMarkRepository _markRepository;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;
		public MarkController(IMarkRepository markRepository, IAuthorizationMiddleware authorizationMiddleware)
		{
			_markRepository = markRepository;
			_authorizationMiddleware = authorizationMiddleware;
		}
		/// <summary>
		/// Method to get all the marks of the database
		/// </summary>
		/// <returns>List of marks</returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Mark>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllMarks()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "MRK-2001");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "MRK-1101");
			
			var marks = await _markRepository.GetAllMark();
			if (marks.Count == 0)
				throw new NotFoundException("There is no marks in the database, please try again", "MRK-1401");
			return Ok(marks);
		}

		/// <summary>
		/// Method to get a mark based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200,Type = typeof(Mark))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetMarkById(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "MRK-2001");
			
			if (!await _markRepository.MarkExists(id))
				throw new NotFoundException("The mark with the id : " + id + " doesn't exist", "MRK-1002");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "MRK-1402");
			
			var mark = await _markRepository.GetMarkById(id);
			return Ok(mark);
		}
		
		/// <summary>
		/// Method to get the marks based on the user id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("{id}/user")]
		[ProducesResponseType(200, Type = typeof(List<Mark>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetMarkByUser(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "MRK-2001");
			
			var marks = await _markRepository.GetMarkByUser(id);
			if (marks.Count == 0)
				throw new NotFoundException("There is no marks for the user with the id : " + id, "MRK-1403");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "MRK-1101");
			
			return Ok(marks);
		}
		
		/// <summary>
		/// Get the list of mark of the product
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("{id}/product")]
		[ProducesResponseType(200, Type = typeof(List<Mark>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetMarkByProduct(int id)
		{
			var marks = await _markRepository.GetMarkByProduct(id);
			if (marks.Count == 0)
				throw new NotFoundException("There is no marks for the product with the id : " + id, "MRK-1404");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "MRK-1101");
			return Ok(marks);
		}

		/// <summary>
		/// Method to create a mark 
		/// </summary>
		/// <param name="markDto"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateMark([FromBody] CreateMarkDTO markDto)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
			    !await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "MRK-2001");
			
			if (markDto == null)
				throw new BadRequestException("There is missing fields, please try again with some data", "MRK-1102");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "MRK-1101");
			
			await _markRepository.CreateMark(markDto);
			return Ok();
		}

		/// <summary>
		/// Method to update a mark based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updateMarkDto"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateMark(int id, [FromBody] UpdateMarkDTO updateMarkDto)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "MRK-2001");
			
			if (updateMarkDto == null)
				throw new BadRequestException("There is missing fields, please try again with some data", "MRK-1102");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "MRK-1101");
			
			if (!await _markRepository.MarkExists(id))
				throw new NotFoundException("The mark with the id : " + id + " doesn't exist", "MRK-1402");
			
			await _markRepository.UpdateMarkById(updateMarkDto, id);
			return Ok();
		}

		/// <summary>
		/// Method to delete a mark based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteMark(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "MRK-2001");
			
			if (!await _markRepository.MarkExists(id))
				throw new NotFoundException("The mark with the id : " + id + " doesn't exist", "MRK-1402");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured","MRK-1101");
			
			await _markRepository.DeleteMarkById(id);
			return Ok();
		}
	}
}
