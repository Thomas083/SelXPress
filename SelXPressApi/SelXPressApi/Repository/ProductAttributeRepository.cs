using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.ProductAttributeDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;
        private readonly IMapper _mapper;

        public ProductAttributeRepository(DataContext context, ICommonMethods commonMethods, IMapper mapper)
        {
            _context = context;
            _commonMethods = commonMethods;
            _mapper = mapper;
        }

        // Check if a product attribute with the given ID exists
        public async Task<bool> ProductAttributeExists(int id)
        {
            return await _context.ProductAttributes.AnyAsync(pa => pa.Id == id);
        }

        // Create a new product attribute with the provided data
        public async Task<bool> CreateProductAttribute(CreateProductAttributeDTO createProductAttribute)
        {
            var productAttributeEntity = _mapper.Map<ProductAttribute>(createProductAttribute);

            _context.ProductAttributes.Add(productAttributeEntity);
            return await _commonMethods.Save();
        }

        // Delete a product attribute by its ID if it exists
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

        // Get all product attributes
        public async Task<List<ProductAttribute>> GetAllProductAttributes()
        {
            var productAttributes = await _context.ProductAttributes.ToListAsync();
            return productAttributes;
        }

        // Get a product attribute by its ID
        public async Task<ProductAttribute?> GetProductAttributeById(int id)
        {
            var productAttribute = await _context.ProductAttributes
                .FirstOrDefaultAsync(pa => pa.Id == id);

            return productAttribute;
        }

        // Update an existing product attribute with the provided data
        public async Task<bool> UpdateProductAttribute(int id, UpdateProductAttributeDTO updateProductAttribute)
        {
            if (!await ProductAttributeExists(id))
                return false;

            var productAttribute = await _context.ProductAttributes.FirstOrDefaultAsync(pa => pa.Id == id);

            if (productAttribute == null)
                return false;

            // Use the mapper to apply changes from the update DTO
            _mapper.Map(updateProductAttribute, productAttribute);

            // Update other properties if necessary
            // productAttribute.SomeProperty = updateProductAttribute.SomeProperty;

            _context.ProductAttributes.Update(productAttribute);
            return await _commonMethods.Save();
        }
    }
}
