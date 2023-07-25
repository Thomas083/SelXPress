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
                Stock = product.Stock,

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
                Stock= product.Stock,
            }).FirstOrDefault();
        }

        public async Task<bool> UpdateProduct(UpdateProductDTO updateProduct, int id)
        {
            if (!await ProductExists(id))
                return false;
            Product? product = _context.Products.Where(p =>p.Id == id).FirstOrDefault();

            if (product != null && updateProduct.Name != null && product.Name != updateProduct.Name)
                await _context.Products.Where(p => p.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Name, x => updateProduct.Name));
            if (product != null && updateProduct.Price != null && product.Price != updateProduct.Price)
                await _context.Products.Where(p => p.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Price, x => updateProduct.Price));
            if (product != null && updateProduct.Description != null && product.Description != updateProduct.Description)
                await _context.Products.Where(p => p.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Description, x => updateProduct.Description));
            if (product != null && updateProduct.Picture != null && product.Picture != updateProduct.Picture)
                await _context.Products.Where(p => p.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Picture, x => updateProduct.Picture));
            if (product != null && updateProduct.Category != null && product.Category != updateProduct.Category)
                await _context.Products.Where(p => p.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Category, x => updateProduct.Category));
            if (product != null && updateProduct.Stock != null && product.Stock != updateProduct.Stock)
                await _context.Products.Where(p => p.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Stock, x => updateProduct.Stock));
            
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            if(await ProductExists(id))
            {
                await _context.Products.Where(p => p.Id != id).ExecuteDeleteAsync();
                var saved = await _context.SaveChangesAsync();
                return saved > 0;
            }
            return false;
        }
    }
}
