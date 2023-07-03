using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        private readonly DataContext _context;

        public ProductAttributeRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<ProductAttribute> GetAllProductAttributes()
        {
            return _context.ProductAttributes.OrderBy(pa => pa.Id).ToList();
        }
    }
}
