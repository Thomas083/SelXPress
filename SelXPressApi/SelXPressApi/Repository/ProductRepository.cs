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

        public ICollection<Product> GetAllProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Where(p => p.Id == id).FirstAsync();
        }
    }
}
