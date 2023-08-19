using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;
        private readonly IMapper _mapper;

        public ProductRepository(DataContext context, ICommonMethods commonMethods, IMapper mapper)
        {
            _context = context;
            _commonMethods = commonMethods;
            _mapper = mapper;
        }
        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> CreateProduct(CreateProductDTO createProduct)
        {
            var productEntity = _mapper.Map<Product>(createProduct);

            // Create ProductAttributes based on the provided AttributeIds
            if (createProduct.AttributeIds != null && createProduct.AttributeIds.Any())
            {
                var productAttributes = createProduct.AttributeIds.Select(attributeId => new ProductAttribute
                {
                    AttributeId = attributeId
                }).ToList();

                productEntity.ProductAttributes = productAttributes;
            }

            _context.Products.Add(productEntity);
            return await _commonMethods.Save();
        }


        public async Task<bool> DeleteProduct(int id)
        {
            if (await ProductExists(id))
            {
                var product = await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
                return await _commonMethods.Save();
            }
            return false;
        }

        public async Task<List<Product>> GetAllProducts(string categoryName, List<string> tagNames, int pageNumber, int pageSize)
        {
            var query = _context.Products.Include(p => p.Category).Include(p => p.ProductAttributes)
                              .Where(p => categoryName == null || p.Category.Name == categoryName)
                              .Where(p => tagNames == null || tagNames.All(t => p.ProductAttributes.Any(pa => pa.Attribute.Name == t)))
                              .OrderBy(p => p.Id);

            var totalCount = await query.CountAsync();
            var products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return products;
        }


        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }


        public async Task<bool> UpdateProduct(int id, UpdateProductDTO updateProductDTO)
        {
            if (!await ProductExists(id))
                return false;
            var product = await _context.Products.FindAsync(id);

            _mapper.Map(updateProductDTO, product);

            await _commonMethods.Save();

            return true;
        }

    }
}
