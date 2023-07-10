using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly DataContext _context;

        public OrderProductRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<OrderProduct> GetAllOrderProducts()
        {
            return _context.OrderProducts.OrderBy(op => op.Id).ToList();
        }
    }
}
