using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.TagDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationMiddleware _authorizationMiddleware;

        public TagController(ITagRepository tagRepository, IMapper mapper, IAuthorizationMiddleware authorizationMiddleware)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
            _authorizationMiddleware = authorizationMiddleware;
        }

        /// <summary>
        /// Method to get all the tags on the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TagDto>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetTags()
        {
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is invalid, a bad request occurred", "TAG-1101");

            var tags = _mapper.Map<List<TagDto>>(await _tagRepository.GetAllTags());

            if (tags.Count == 0)
                throw new NotFoundException("No tags found in the database", "TAG-1401");

            return Ok(tags);
        }

        /// <summary>
        /// Method to get a tag based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="BadRequestException"></exception>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TagDto))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetTag(int id)
        {
            if (!await _tagRepository.TagExists(id))
                throw new NotFoundException($"Tag with ID: {id} does not exist", "TAG-1402");

            if (!ModelState.IsValid)
                throw new BadRequestException("The model is invalid, a bad request occurred", "TAG-1101");

            var tag = _mapper.Map<TagDto>(await _tagRepository.GetTagById(id));
            return Ok(tag);
        }

        /// <summary>
        /// Method to create a tag
        /// </summary>
        /// <param name="newTag"></param>
        /// <returns></returns>
        /// <exception cref="ForbiddenRequestException"></exception>
        /// <exception cref="BadRequestException"></exception>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagDTO newTag)
        {
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
            if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
                throw new ForbiddenRequestException("You are not authorized to do this operation", "TAG-2001");
            
            if (newTag.Name == null)
                throw new BadRequestException("Missing fields, please provide valid data", "TAG-1102");
            
            await _tagRepository.CreateTag(newTag);
            return StatusCode(201);
        }

        /// <summary>
        /// Method to update a tag based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tagUpdate"></param>
        /// <returns></returns>
        /// <exception cref="ForbiddenRequestException"></exception>
        /// <exception cref="BadRequestException"></exception>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] UpdateTagDTO tagUpdate)
        {
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
            if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
                throw new ForbiddenRequestException("You are not authorized to do this operation", "TAG-2001");
            
            if (tagUpdate == null)
                throw new BadRequestException("Missing fields, please provide valid data", "TAG-1102");

            if (!ModelState.IsValid)
                throw new BadRequestException("The model is invalid, a bad request occurred", "TAG-1101");

            if (!await _tagRepository.TagExists(id))
                throw new NotFoundException($"Tag with ID: {id} does not exist", "TAG-1402");

            if (!await _tagRepository.UpdateTag(id, tagUpdate))
                return Ok();

            throw new Exception("An error occurred while updating the tag");
        }

        /// <summary>
        /// Method to delete a tag based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ForbiddenRequestException"></exception>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="BadRequestException"></exception>
        /// <exception cref="Exception"></exception>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
            if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
                throw new ForbiddenRequestException("You are not authorized to do this operation", "TAG-2001");
            
            if (!await _tagRepository.TagExists(id))
                throw new NotFoundException($"Tag with ID: {id} does not exist", "TAG-1402");

            if (!ModelState.IsValid)
                throw new BadRequestException("The model is invalid, a bad request occurred", "TAG-1101");

            if (!await _tagRepository.DeleteTag(id))
                return Ok();

            throw new Exception("An error occurred while deleting the tag");
        }
    }
}
