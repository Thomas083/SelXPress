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

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TagDTO>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetTags()
        {
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occurred", "TAG-1101");

            var tags = _mapper.Map<List<TagDTO>>(await _tagRepository.GetAllTags());

            if (tags.Count == 0)
                throw new NotFoundException("There are no tags in the database, please try again", "TAG-1401");

            return Ok(tags);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TagDTO))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetTag(int id)
        {
            if (!await _tagRepository.TagExists(id))
                throw new NotFoundException("The tag with the id: " + id + " doesn't exist", "TAG-1402");
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occurred", "TAG-1101");

            var tag = _mapper.Map<TagDTO>(await _tagRepository.GetTagById(id));
            return Ok(tag);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagDTO newTag)
        {
            if (newTag.Name == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "TAG-1102");

            if (await _tagRepository.CreateTag(newTag))
                return StatusCode(201);
            throw new Exception("An error occurred while creating the tag");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] UpdateTagDTO tagUpdate)
        {
            if (tagUpdate == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "TAG-1102");
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occurred", "TAG-1101");
            if (!await _tagRepository.TagExists(id))
                throw new NotFoundException("The tag with the id: " + id + " doesn't exist", "TAG-1402");
            if (!await _tagRepository.UpdateTag(id, tagUpdate))
                return Ok();
            throw new Exception("An error occurred while updating the tag");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteTag(int id)
        {
            if (!await _tagRepository.TagExists(id))
                throw new NotFoundException("The tag with the id: " + id + " doesn't exist", "TAG-1402");
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occurred", "TAG-1101");
            if (!await _tagRepository.DeleteTag(id))
                return Ok();
            throw new Exception("An error occurred while deleting the tag");
        }
    }
}