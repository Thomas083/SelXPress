using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.ProductAttributeDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributeController : ControllerBase
    {
        private readonly IAuthorizationMiddleware _authorizationMiddleware;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IMapper _mapper;

        public ProductAttributeController(IAuthorizationMiddleware authorizationMiddleware, IProductAttributeRepository productAttributeRepository, IMapper mapper)
        {
            _authorizationMiddleware = authorizationMiddleware;
            _productAttributeRepository = productAttributeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all product attributes
        /// </summary>
        /// <returns>An array of all product attributes</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProductAttributeDTO>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetProductAttributes()
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

                var productAttributes = await _productAttributeRepository.GetAllProductAttributes();

                if (productAttributes.Count == 0)
                    throw new NotFoundException("There are no Product Attributes in the database", "PAT-1401");

                return Ok(productAttributes);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, new InternalServerErrorTemplate());
            }
        }

        /// <summary>
        /// Get a product attribute by ID
        /// </summary>
        /// <param name="id">The ID of the product attribute</param>
        /// <returns>The specific product attribute</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProductAttributeDTO))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

                var productAttribute = await _productAttributeRepository.GetProductAttributeById(id);

                if (productAttribute == null)
                    throw new NotFoundException("The product attribute with the given ID does not exist", "PAT-1401");

                return Ok(productAttribute);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, new InternalServerErrorTemplate());
            }
        }

        /// <summary>
        /// Create a new product attribute
        /// </summary>
        /// <param name="productAttribute">The product attribute to create</param>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateProductAttribute([FromBody] CreateProductAttributeDTO productAttribute)
        {
            try
            {
                await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

                if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
                    !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
                {
                    throw new ForbiddenRequestException("You are not authorized to perform this operation", "PAT-2001");
                }

                if (productAttribute == null)
                    throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

                await _productAttributeRepository.CreateProductAttribute(productAttribute);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, new InternalServerErrorTemplate());
            }
        }

        /// <summary>
        /// Modify a product attribute
        /// </summary>
        /// <param name="id">The ID of the product attribute to modify</param>
        /// <param name="updateProductAttribute">The updated product attribute data</param>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateProductAttribute(int id, [FromBody] UpdateProductAttributeDTO updateProductAttribute)
        {
            try
            {
                await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

                if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
                    !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
                {
                    throw new ForbiddenRequestException("You are not authorized to perform this operation", "PAT-2001");
                }

                if (!ModelState.IsValid)
                    throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

                if (updateProductAttribute == null)
                    throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

                var result = await _productAttributeRepository.UpdateProductAttribute(id, updateProductAttribute);

                if (!result)
                    throw new NotFoundException("The product attribute with the given ID does not exist", "PAT-1401");

                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, new InternalServerErrorTemplate());
            }
        }

        /// <summary>
        /// Delete a product attribute
        /// </summary>
        /// <param name="id">The ID of the product attribute to delete</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteProductAttribute(int id)
        {
            try
            {
                await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

                if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
                    !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
                {
                    throw new ForbiddenRequestException("You are not authorized to perform this operation", "PAT-2001");
                }

                if (!ModelState.IsValid)
                    throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

                if (!await _productAttributeRepository.ProductAttributeExists(id))
                    throw new NotFoundException("The product attribute with the given ID does not exist", "PAT-1401");

                await _productAttributeRepository.DeleteProductAttribute(id);
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, new InternalServerErrorTemplate());
            }
        }
    }
}
