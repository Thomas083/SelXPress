using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProduct(CreateProductDTO product)
        {
            Product newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Picture = product.Picture,
                Stock = product.Stock,
            };
            await _context.Products.AddAsync(newProduct);
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public ICollection<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Where(p => p.Id == id).FirstAsync();
        }
    }
}
