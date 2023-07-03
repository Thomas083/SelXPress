using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetAllOrders();
    }
}
