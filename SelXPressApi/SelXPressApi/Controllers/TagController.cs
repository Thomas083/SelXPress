using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.TagDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;

namespace SelXPressApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all tags.
        /// </summary>
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
        /// Get a specific tag by ID.
        /// </summary>
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
        /// Create a new tag.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagDTO newTag)
        {
            if (newTag.Name == null)
                throw new BadRequestException("Missing fields, please provide valid data", "TAG-1102");

            if (await _tagRepository.CreateTag(newTag))
                return StatusCode(201);

            throw new Exception("An error occurred while creating the tag");
        }

        /// <summary>
        /// Update a tag by ID.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] UpdateTagDTO tagUpdate)
        {
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
        /// Delete a tag by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteTag(int id)
        {
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
