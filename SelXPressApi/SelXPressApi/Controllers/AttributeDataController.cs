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

		/// <summary>
		/// Initializes a new instance of the <see cref="AttributeDataController"/> class.
		/// </summary>
		/// <param name="attributeDataRepository">The repository for managing attribute data.</param>
		/// <param name="mapper">The AutoMapper instance for object mapping.</param>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		public AttributeDataController(IAttributeDataRepository attributeDataRepository, IMapper mapper, IAuthorizationMiddleware authorizationMiddleware)
        {
            _attributeDataRepository = attributeDataRepository;
            _mapper = mapper;
            _authorizationMiddleware = authorizationMiddleware;
        }

		#region Get Methods
		/// <summary>
		/// Get all attribute data from the database.
		/// </summary>
		/// <returns>Returns an array of all attribute data.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when no attribute data are found in the database.</exception>
		[HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AttributeDataDto>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttributesDatas()
        {
			// Check if the model state is valid
            if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");

			// Retrieve all attributes Datas from the repository
            var attributesDatas = _mapper.Map<List<AttributeDataDto>>(await _attributeDataRepository.GetAllAttributesData());

			// Check if any attributes Datas were found
			if (attributesDatas.Count == 0)
				throw new NotFoundException("There is no AttributeData in the database, please try again", "ATD-1401");

            return Ok(attributesDatas);
        }

		/// <summary>
		/// Get attribute data based on the id.
		/// </summary>
		/// <param name="id">The id of the attribute data to retrieve.</param>
		/// <returns>Returns the attribute data with the specified id.</returns>
		/// <exception cref="NotFoundException">Thrown when the attribute data with the specified id is not found.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		[HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AttributeDataDto))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttributeData(int id)
        {
			// Check if the attribute with the given ID exists
            if(!await _attributeDataRepository.AttributeDataExists(id))
				throw new NotFoundException("The AttributeData with the id : " + id + " doesn't exist", "ATD-1402");

			// Check if the model state is valid
            if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");
			// Retrieve the attributeData by its ID
            var attributeData = await _attributeDataRepository.GetAttributeDataById(id);
			return Ok(attributeData);
        }
        #endregion

		#region Create Methods
		/// <summary>
		/// Create attribute data.
		/// </summary>
		/// <param name="attributeData">The data for creating the attribute data.</param>
		/// <returns>Returns 201 Created if the attribute data is successfully created.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or required fields are missing.</exception>
		[HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateAttributeData([FromBody] CreateAttributeDataDTO attributeData)
        {
			// Check if a valid token exists in the HttpContext
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ATD-2001");
            
			// Check if the provided attribute data is complete
            if (attributeData == null || attributeData.Value == null || attributeData.Key == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ATD-1102");

			// Create the attribute using the repository
            await _attributeDataRepository.CreateAttributeData(attributeData);
			return StatusCode(201);
        }
		#endregion

		#region Put Methods
		/// <summary>
		/// Update attribute data based on the id.
		/// </summary>
		/// <param name="id">The id of the attribute data to update.</param>
		/// <param name="attributeData">The data for updating the attribute data.</param>
		/// <returns>Returns 200 OK if the attribute data is successfully updated.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or required fields are missing.</exception>
		/// <exception cref="NotFoundException">Thrown when the attribute data with the specified id is not found.</exception>
		[HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateAttributeData(int id, [FromBody] UpdateAttributeDataDTO attributeData)
        {
			// Check if a valid token exists in the HttpContext
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ATD-2001");
            
			// Check if the model state is valid
            if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");
            
			// Check if the provided attribute data update data is complete
            if(attributeData == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "ATD-1102");
            
			// Check if the attribute data with the given ID exists
            if(!await _attributeDataRepository.AttributeDataExists(id))
				throw new NotFoundException("The AttributeData with the id : " + id + " doesn't exist", "ATD-1402");
            
			// Update the attribute using the repository
            await _attributeDataRepository.UpdateAttributeData(id, attributeData);
			return Ok();
        }
        #endregion

		#region Delete Methods
		/// <summary>
		/// Delete attribute data based on the id.
		/// </summary>
		/// <param name="id">The id of the attribute data to delete.</param>
		/// <returns>Returns 200 OK if the attribute data is successfully deleted.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform the operation.</exception>
		/// <exception cref="NotFoundException">Thrown when the attribute data with the specified id is not found.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		[HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteAttributeData(int id)
        {
			// Check if a valid token exists in the HttpContext
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "ATD-2001");
            
			// Check if the attribute data with the given ID exists
            if(!await _attributeDataRepository.AttributeDataExists(id))
				throw new NotFoundException("The AttributeData with the id : " + id + " doesn't exist", "ATD-1402");
            
			// Check if the model state is valid
            if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "ATD-1101");

			// Delete the attribute data using the repository
            await _attributeDataRepository.DeleteAttributeData(id);
			return Ok();
        }
		#endregion
	}
}
