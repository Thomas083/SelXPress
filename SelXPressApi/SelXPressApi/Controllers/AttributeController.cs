using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AttributeController : ControllerBase
	{
		private readonly IAttributeRepository _attributeRepository;
		private readonly IMapper _mapper;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		public AttributeController(IAttributeRepository attributeRepository, IMapper mapper, IAuthorizationMiddleware authorizationMiddleware)
		{
			_attributeRepository = attributeRepository;
			_mapper = mapper;
			_authorizationMiddleware = authorizationMiddleware;
		}
		/// <summary>
		/// Method to get all the attribute of the database
		/// </summary>
		/// <returns></returns>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AttributeDTO>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttributes()
		{
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATT-1101");

			var attributes = await _attributeRepository.GetAllAttributes();
			if(attributes.Count == 0)
                throw new NotFoundException("There is no Attribute in the database, please try again", "ATT-1401");
			return Ok(attributes);
        }

		/// <summary>
		/// Method to get an attribute based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("{id}")]

        [ProducesResponseType(200, Type = typeof(AttributeDTO))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttribute(int id)
        {
			if(!await _attributeRepository.AttributeExists(id))
                throw new NotFoundException("The Attribute with the id : " + id + " doesn't exist", "ATT-1402");

            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATT-1101");

			var attribute = await _attributeRepository.GetAttributeById(id);
			return Ok(attribute);

        }

		/// <summary>
		/// Method to create an attribute
		/// </summary>
		/// <param name="attribute"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateAttribute([FromBody] CreateAttributeDTO attribute)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
			if(attribute == null || attribute.Name == null || attribute.Type == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "ATT-1102");
			
            await _attributeRepository.CreateAttribute(attribute);
			return StatusCode(201);
        }

		/// <summary>
		/// Method to update an attribute based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="attributeUpdate"></param>
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
        public async Task<IActionResult> UpdateAttribute(int id, [FromBody] UpdateAttributeDTO attributeUpdate)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
			
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATT-1101");
            
			if(attributeUpdate == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "ATT-1102");
			
			if (!await _attributeRepository.AttributeExists(id))
				throw new NotFoundException("The Attribute with the id : " + id + " doesn't exist", "ATT-1402");
			
			await _attributeRepository.UpdateAttribute(id, attributeUpdate);
			return Ok();
        }

        /// <summary>
        /// Method to delete an attribute based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ForbiddenRequestException"></exception>
        /// <exception cref="BadRequestException"></exception>
        /// <exception cref="NotFoundException"></exception>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteAttribute(int id)
        {
	        await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
	        if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
		        throw new ForbiddenRequestException("You are not authorized to do this operation", "todo");
	        
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATT-1101");
            
            if (!await _attributeRepository.AttributeExists(id))
                throw new NotFoundException("The Attribute with the id : " + id + " doesn't exist", "ATT-1402");
            
            await _attributeRepository.DeleteAttribute(id);
            return Ok();
        }
    }
}
