using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;

        public CartRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Cart> GetAllCarts()
        {
            return _context.Carts.OrderBy(c => c.Id).ToList();
        }
    }
}
