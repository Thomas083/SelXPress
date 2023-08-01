using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeDataController : ControllerBase
    {
        private readonly IAttributeDataRepository _attributeDataRepository;
        private readonly IMapper _mapper;

        public AttributeDataController(IAttributeDataRepository attributeDataRepository, IMapper mapper)
        {
            _attributeDataRepository = attributeDataRepository;
            _mapper = mapper;
        }
        // GET: api/<AttributeDataController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AttributeDataController>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttributesDatas()
        {
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");

            var attributesDatas = await _attributeDataRepository.GetAllAttributesData();

            if(attributesDatas.Count == 0)
                throw new NotFoundException("There is no AttributeData in the database, please try again", "ATD-1401");
            return Ok(attributesDatas);
        }

        // GET api/<AttributeDataController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AttributeData))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttributeData(int id)
        {
            if(!await _attributeDataRepository.AttributeDataExists(id))
                throw new NotFoundException("The role with the id : " + id + " doesn't exist", "ATD-1402");
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");
            var attributeData = await _attributeDataRepository.GetAttributeDataById(id);
            return Ok(attributeData);
        }

        // POST api/<AttributeDataController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateAttributeData([FromBody] CreateAttributeDataDTO attributeData)
        {
            if (attributeData == null || attributeData.Value == null || attributeData.Key == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "ATD-1102");

            if (!await _attributeDataRepository.CreateAttributeData(attributeData))
                return StatusCode(201);
            throw new Exception("An error occured while the creating of the role");
        }

        // PUT api/<AttributeDataController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateAttributeData(int id, [FromBody] UpdateAttributeDataDTO attributeData)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");
            if(attributeData == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "ATD-1102");
            if(!await _attributeDataRepository.AttributeDataExists(id))
                throw new NotFoundException("The AttributeData with the id : " + id + " doesn't exist", "RLE-1402");
            await _attributeDataRepository.UpdateAttributeData(id, attributeData);
            return Ok();
        }

        // DELETE api/<AttributeDataController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteAttributeData(int id)
        {
            if(!await _attributeDataRepository.AttributeDataExists(id))
                throw new NotFoundException("The AttributeData with the id : " + id + " doesn't exist", "RLE-1402");
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");
            await _attributeDataRepository.DeleteAttributeData(id);
            return Ok();
        }
    }
}
