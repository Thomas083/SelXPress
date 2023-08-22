using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.ProductAttributeDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Repository
{
	/// <summary>
	/// Repository for managing product attributes.
	/// </summary>
	public class ProductAttributeRepository : IProductAttributeRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;
        private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductAttributeRepository"/> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		/// <param name="commonMethods">Common methods interface.</param>
		/// <param name="mapper">Automapper instance for object mapping.</param>
		public ProductAttributeRepository(DataContext context, ICommonMethods commonMethods, IMapper mapper)
		{
			_context = context;
			_commonMethods = commonMethods;
			_mapper = mapper;
		}

		/// <summary>
		/// Checks if a product attribute with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the product attribute to check.</param>
		/// <returns><c>true</c> if the product attribute exists; otherwise, <c>false</c>.</returns>
		public async Task<bool> ProductAttributeExists(int id)
		{
			return await _context.ProductAttributes.AnyAsync(pa => pa.Id == id);
		}

		/// <summary>
		/// Creates a new product attribute.
		/// </summary>
		/// <param name="createProductAttribute">The data to create the product attribute.</param>
		/// <returns><c>true</c> if the product attribute is successfully created; otherwise, <c>false</c>.</returns>
		public async Task<bool> CreateProductAttribute(CreateProductAttributeDTO createProductAttribute)
		{
			var productAttributeEntity = _mapper.Map<ProductAttribute>(createProductAttribute);

			_context.ProductAttributes.Add(productAttributeEntity);
			return await _commonMethods.Save();
		}


		/// <summary>
		/// Deletes a product attribute with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the product attribute to be deleted.</param>
		/// <returns>
		/// <c>true</c> if the product attribute is successfully deleted;
		/// <c>false</c> if the specified ID doesn't exist.
		/// </returns>
		public async Task<bool> DeleteProductAttribute(int id)
		{
			if (await ProductAttributeExists(id))
			{
				var productAttribute = await _context.ProductAttributes.FindAsync(id);
				_context.ProductAttributes.Remove(productAttribute);
				return await _commonMethods.Save();
			}
			return false;
		}

		/// <summary>
		/// Retrieves a list of all product attributes.
		/// </summary>
		/// <returns>A list of all product attributes.</returns>
		public async Task<List<ProductAttribute>> GetAllProductAttributes()
		{
			var productAttributes = await _context.ProductAttributes.ToListAsync();
			return productAttributes;
		}

		/// <summary>
		/// Retrieves a product attribute by its ID.
		/// </summary>
		/// <param name="id">The ID of the product attribute to retrieve.</param>
		/// <returns>The retrieved product attribute, or null if not found.</returns>
		public async Task<ProductAttribute?> GetProductAttributeById(int id)
		{
			var productAttribute = await _context.ProductAttributes
				.FirstOrDefaultAsync(pa => pa.Id == id);

			return productAttribute;
		}

		/// <summary>
		/// Updates an existing product attribute with new information.
		/// </summary>
		/// <param name="id">The ID of the product attribute to update.</param>
		/// <param name="updateProductAttribute">Updated information for the product attribute.</param>
		/// <returns>True if the update was successful, otherwise false.</returns>
		public async Task<bool> UpdateProductAttribute(int id, UpdateProductAttributeDTO updateProductAttribute)
		{
			if (!await ProductAttributeExists(id))
				return false;

			var productAttribute = await _context.ProductAttributes.FirstOrDefaultAsync(pa => pa.Id == id);

			if (productAttribute == null)
				return false;

			_mapper.Map(updateProductAttribute, productAttribute);

			_context.ProductAttributes.Update(productAttribute);
			return await _commonMethods.Save();
		}
	}
}
