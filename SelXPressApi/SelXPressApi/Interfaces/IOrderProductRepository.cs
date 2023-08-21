using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	public interface IOrderProductRepository
	{
		/// <summary>
		/// Check if an order product with the specified ID exists.
		/// </summary>
		Task<bool> OrderProductExists(int id);

		/// <summary>
		/// Get all order products associated with a user's email.
		/// </summary>
		Task<List<OrderProduct>> GetOrderProductsByUser(string email);

		/// <summary>
		/// Get an order product by its ID.
		/// </summary>
		Task<OrderProduct> GetOrderProductById(int id);

		/// <summary>
		/// Get all order products.
		/// </summary>
		Task<List<OrderProduct>> GetAllOrderProducts();

		/// <summary>
		/// Create a new order product.
		/// </summary>
		Task<bool> CreateOrderProduct(CreateOrderProductDTO createOrderProduct);

		/// <summary>
		/// Update an existing order product.
		/// </summary>
		Task<bool> UpdateOrderProduct(int id, UpdateOrderProductDTO updateOrderProduct);

		/// <summary>
		/// Delete an order product with the specified ID.
		/// </summary>
		Task<bool> DeleteOrderProduct(int id);
	}
}
