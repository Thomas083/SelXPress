using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	public interface IOrderRepository
	{
		/// <summary>
		/// Check if an order with the specified ID exists.
		/// </summary>
		Task<bool> OrderExists(int id);

		/// <summary>
		/// Get an order by its ID.
		/// </summary>
		Task<Order> GetOrderById(int id);

		/// <summary>
		/// Get all orders.
		/// </summary>
		Task<List<Order>> GetAllOrders();

		/// <summary>
		/// Create a new order.
		/// </summary>
		Task<bool> CreateOrder(CreateOrderDTO createOrder);

		/// <summary>
		/// Update an existing order.
		/// </summary>
		Task<bool> UpdateOrder(int id, UpdateOrderDTO updateOrder);

		/// <summary>
		/// Delete an order with the specified ID.
		/// </summary>
		Task<bool> DeleteOrder(int id);
	}
}
