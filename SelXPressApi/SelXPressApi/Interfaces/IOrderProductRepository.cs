using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IOrderProductRepository
    {
        ICollection<OrderProduct> GetAllOrderProducts();
    }
}
