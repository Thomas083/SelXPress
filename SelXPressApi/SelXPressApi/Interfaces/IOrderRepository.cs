using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Interface for interacting with order-related data in the repository.
	/// </summary>
	public interface IOrderRepository
	{
		/// <summary>
		/// Checks if an order with the specified ID exists.
		/// </summary>
		/// <param name="id">The unique identifier of the order.</param>
		/// <returns>True if the order exists, false otherwise.</returns>
		Task<bool> OrderExists(int id);

		/// <summary>
		/// Retrieves an order by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the order.</param>
		/// <returns>The order associated with the provided ID, or null if not found.</returns>
		Task<Order> GetOrderById(int id);

		/// <summary>
		/// Retrieves a list of all orders stored in the repository.
		/// </summary>
		/// <returns>A list of all orders.</returns>
		Task<List<Order>> GetAllOrders();

		/// <summary>
		/// Creates a new order.
		/// </summary>
		/// <param name="createOrder">The DTO containing order creation information.</param>
		/// <returns>True if the order was successfully created, false otherwise.</returns>
		Task<bool> CreateOrder(CreateOrderDTO createOrder);

		/// <summary>
		/// Updates an existing order with new information.
		/// </summary>
		/// <param name="id">The unique identifier of the order to be updated.</param>
		/// <param name="updateOrder">The DTO containing updated order information.</param>
		/// <returns>True if the order was successfully updated, false otherwise.</returns>
		Task<bool> UpdateOrder(int id, UpdateOrderDTO updateOrder);

		/// <summary>
		/// Deletes an order by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the order to be deleted.</param>
		/// <returns>True if the order was successfully deleted, false otherwise.</returns>
		Task<bool> DeleteOrder(int id);
	}
}
