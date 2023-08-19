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
        public async Task<bool> ProductAttributeExists(int id)
        {
            return await _context.ProductAttributes.AnyAsync(pa => pa.Id == id);
        }

        public async Task<bool> CreateProductAttribute(CreateProductAttributeDTO createProductAttribute)
        {
            var productAttributeEntity = _mapper.Map<ProductAttribute>(createProductAttribute);

            _context.ProductAttributes.Add(productAttributeEntity);
            return await _commonMethods.Save();
        }

        public async Task<bool> DeleteProductAttribute(int id)
        {

            if(await ProductAttributeExists(id))
            {
                var productAttribute = await _context.ProductAttributes.FindAsync(id);
                _context.ProductAttributes.Remove(productAttribute);
                return await _commonMethods.Save();
            }
            return false;
        }

        public async Task<List<ProductAttribute>> GetAllProductAttributes()
        {
            var productAttributes = await _context.ProductAttributes.ToListAsync();
            return productAttributes;
        }

        public async Task<ProductAttribute?> GetProductAttributeById(int id)
        {
            var productAttribute = await _context.ProductAttributes
                .FirstOrDefaultAsync(pa => pa.Id == id);

            return productAttribute;
        }

        public async Task<bool> UpdateProductAttribute(int id, UpdateProductAttributeDTO updateProductAttribute)
        {
            if (!await ProductAttributeExists(id))
                return false;
            var productAttribute = await _context.ProductAttributes.FirstOrDefaultAsync(pa => pa.Id == id);

            if (productAttribute == null)
                return false;

            // Utilisez le mapper pour appliquer les modifications depuis le DTO de mise à jour
            _mapper.Map(updateProductAttribute, productAttribute);

            // Mettez à jour d'autres propriétés si nécessaire
            // productAttribute.SomeProperty = updateProductAttribute.SomeProperty;

            _context.ProductAttributes.Update(productAttribute);
            return await _commonMethods.Save();
        }
    }
}
