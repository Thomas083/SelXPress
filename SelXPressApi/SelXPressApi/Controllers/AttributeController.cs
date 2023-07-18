using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AttributeController : ControllerBase
	{
		private readonly IAttributeRepository _attributeRepository;
		private readonly IMapper _mapper;

		public AttributeController(IAttributeRepository attributeRepository, IMapper mapper)
		{
			_attributeRepository = attributeRepository;
			_mapper = mapper;
		}
		/// <summary>
		/// GET: api/<AttributeController>
		/// Get all attributes
		/// </summary>
		/// <returns>Return an Array of all attributes</returns>
		[HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AttributeDTO>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttributes()
		{

		}

		/// <summary>
		/// GET api/<AttributeController>/5
		/// Get an attribute by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a specific attribute</returns>
		[HttpGet("{id}")]

        [ProducesResponseType(200, Type = typeof(AttributeDTO))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttribute(int id)
        {

		}

		/// <summary>
		/// POST api/<AttributeController>
		/// Create new attribute
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateAttribute([FromBody] CreateUserDto newUser)
        {

		}

		/// <summary>
		/// PUT api/<AttributeController>/5
		/// Modify an attribute
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateAttribute(int id, [FromBody] UpdateAttributeDTO attributeUpdate)
        {

		}

		/// <summary>
		/// DELETE api/<AttributeController>/5
		/// Delete an attribute
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteAttribute(int id)
        {

		}
	}
}
