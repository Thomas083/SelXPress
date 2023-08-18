using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
    /// <summary>
    /// Controller for managing AttributeData.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeDataController : ControllerBase
    {
        private readonly IAttributeDataRepository _attributeDataRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationMiddleware _authorizationMiddleware;

        public AttributeDataController(IAttributeDataRepository attributeDataRepository, IMapper mapper, IAuthorizationMiddleware authorizationMiddleware)
        {
            _attributeDataRepository = attributeDataRepository;
            _mapper = mapper;
            _authorizationMiddleware = authorizationMiddleware;
        }

        /// <summary>
        /// Method to get all the attribute data in the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AttributeDataDto>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttributesDatas()
        {
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");

            var attributesDatas = _mapper.Map<List<AttributeDataDto>>(await _attributeDataRepository.GetAllAttributesData());

            if(attributesDatas.Count == 0)
                throw new NotFoundException("There is no AttributeData in the database, please try again", "ATD-1401");

            return Ok(attributesDatas);
        }

        /// <summary>
        /// Method to get the attribute data based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="BadRequestException"></exception>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AttributeDataDto))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttributeData(int id)
        {
            if(!await _attributeDataRepository.AttributeDataExists(id))
                throw new NotFoundException("The AttributeData with the id : " + id + " doesn't exist", "ATD-1402");
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");
            var attributeData = await _attributeDataRepository.GetAttributeDataById(id);
            return Ok(attributeData);
        }

        /// <summary>
        /// Method to create attribute data
        /// </summary>
        /// <param name="attributeData"></param>
        /// <returns></returns>
        /// <exception cref="ForbiddenRequestException"></exception>
        /// <exception cref="BadRequestException"></exception>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateAttributeData([FromBody] CreateAttributeDataDTO attributeData)
        {
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
            if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
                throw new ForbiddenRequestException("You are not authorized to do this operation", "ATD-2001");
            
            if (attributeData == null || attributeData.Value == null || attributeData.Key == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "ATD-1102");

            await _attributeDataRepository.CreateAttributeData(attributeData);
            return StatusCode(201);
        }

        /// <summary>
        /// Method to update the attribute data based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attributeData"></param>
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
        public async Task<IActionResult> UpdateAttributeData(int id, [FromBody] UpdateAttributeDataDTO attributeData)
        {
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
            if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
                throw new ForbiddenRequestException("You are not authorized to do this operation", "ATD-2001");
            
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");
            
            if(attributeData == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "ATD-1102");
            
            if(!await _attributeDataRepository.AttributeDataExists(id))
                throw new NotFoundException("The AttributeData with the id : " + id + " doesn't exist", "ATD-1402");
            
            await _attributeDataRepository.UpdateAttributeData(id, attributeData);
            return Ok();
        }

        /// <summary>
        /// Method to delete the attribute data based on the id
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
        public async Task<IActionResult> DeleteAttributeData(int id)
        {
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
            if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
                throw new ForbiddenRequestException("You are not authorized to do this operation", "ATD-2001");
            
            if(!await _attributeDataRepository.AttributeDataExists(id))
                throw new NotFoundException("The AttributeData with the id : " + id + " doesn't exist", "ATD-1402");
            
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");
            await _attributeDataRepository.DeleteAttributeData(id);
            return Ok();
        }
    }
}
