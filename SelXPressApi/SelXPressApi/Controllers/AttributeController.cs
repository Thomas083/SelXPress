﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Exceptions;
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
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATT-1101");

			var attributes = await _attributeRepository.GetAllAttributes();
			if(attributes.Count == 0)
                throw new NotFoundException("There is no Attribute in the database, please try again", "ATT-1401");
			return Ok(attributes);
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
			if(!await _attributeRepository.AttributeExists(id))
                throw new NotFoundException("The Attribute with the id : " + id + " doesn't exist", "ATT-1402");

            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATT-1101");

			var attribute = await _attributeRepository.GetAttributeById(id);
			return Ok(attribute);

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
        public async Task<IActionResult> CreateAttribute([FromBody] CreateAttributeDTO attribute)
        {
			if(attribute == null || attribute.Name == null || attribute.Type == null)
                throw new BadRequestException("There are missing fields, please try again with some data", "ATT-1102");
			if (!await _attributeRepository.CreateAttribute(attribute))
				return StatusCode(201);
            throw new Exception("An error occured while the creating of the Attribute");
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
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occured", "ATT-1101");
            if (!await _attributeRepository.AttributeExists(id))
                throw new NotFoundException("The Attribute with the id : " + id + " doesn't exist", "ATT-1402");
            await _attributeRepository.DeleteAttribute(id);
            return Ok();
        }
    }
}
