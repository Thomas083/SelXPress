using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface ICartRepository
    {
        ICollection<Cart> GetAllCarts();
    }
}
