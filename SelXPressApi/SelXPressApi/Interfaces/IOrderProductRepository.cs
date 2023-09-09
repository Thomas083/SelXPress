using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Represents a repository for managing order products.
	/// </summary>
	/// <seealso  cref="Models"/>
	/// <seealso  cref="DTO"/>
	/// <seealso  cref="Controllers"/>
	/// <seealso  cref="Repository"/>
	/// <seealso  cref="Helper"/>
	/// <seealso  cref="DocumentationErrorTemplate"/>
	/// <seealso  cref="Exceptions"/>
	/// <seealso  cref="Interfaces"/>
	/// <seealso  cref="Middleware"/>
	/// <seealso  cref="Data"/>
	public interface IOrderProductRepository
	{
		/// <summary>
		/// Checks if an order product with the specified ID exists.
		/// </summary>
		/// <param name="id">The unique identifier of the order product.</param>
		/// <returns>True if the order product exists, otherwise false.</returns>
		Task<bool> OrderProductExists(int id);

		/// <summary>
		/// Retrieves all order products associated with a user's email.
		/// </summary>
		/// <param name="email">The email address of the user.</param>
		/// <returns>A list of order products associated with the user.</returns>
		Task<List<OrderProduct>> GetOrderProductsByUser(string email);

		/// <summary>
		/// Retrieves an order product by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the order product.</param>
		/// <returns>The order product matching the specified ID, or null if not found.</returns>
		Task<OrderProduct> GetOrderProductById(int id);

		/// <summary>
		/// Retrieves all order products.
		/// </summary>
		/// <returns>A list of all order products.</returns>
		Task<List<OrderProduct>> GetAllOrderProducts();

		/// <summary>
		/// Creates a new order product.
		/// </summary>
		/// <param name="createOrderProduct">The DTO containing order product creation information.</param>
		/// <returns>True if the order product was successfully created, otherwise false.</returns>
		Task<bool> CreateOrderProduct(CreateOrderProductDTO createOrderProduct);

		/// <summary>
		/// Updates an existing order product.
		/// </summary>
		/// <param name="id">The unique identifier of the order product to be updated.</param>
		/// <param name="updateOrderProduct">The DTO containing updated order product information.</param>
		/// <returns>True if the order product was successfully updated, otherwise false.</returns>
		Task<bool> UpdateOrderProduct(int id, UpdateOrderProductDTO updateOrderProduct);

		/// <summary>
		/// Deletes an order product by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the order product to be deleted.</param>
		/// <returns>True if the order product was successfully deleted, otherwise false.</returns>
		Task<bool> DeleteOrderProduct(int id);
	}
}
