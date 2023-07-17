using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
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

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.Join(_context.Categories, product => product.Category.Id, category => category.Id, (product, category) => new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Picture = product.Picture,
                Category = category,

            }).Join(_context.Stocks, product => product.Stock.Id, stock => stock.Id, (product, stock) => new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Picture = product.Picture,
                Category = product.Category,
                Stock = stock,
            }).ToListAsync();
        }

        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<Product?> GetProductById(int id)
        {
            return _context.Products.Where(p => p.Id == id).Join(_context.Categories, product => product.Category.Id, category => category.Id, (product, category) => new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Picture = product.Picture,
                Category = category,

            }).Join(_context.Stocks, product => product.Stock.Id, stock => stock.Id, (product, stock) => new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Picture = product.Picture,
                Category = product.Category,
                Stock = stock,
            }).FirstOrDefault();
        }
    }
}
