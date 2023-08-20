using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	public interface IOrderRepository
	{
		Task<bool> OrderExists(int id);
		Task<Order> GetOrderById(int id);
		Task<List<Order>> GetAllOrders();
		Task<bool> CreateOrder(CreateOrderDTO createOrder);
		Task<bool> UpdateOrder(int id, UpdateOrderDTO updateOrder);
		Task<bool> DeleteOrder(int id);
	}
}
