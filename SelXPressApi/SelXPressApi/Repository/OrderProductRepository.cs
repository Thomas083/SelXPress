using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SelXPressApi.Repository
{
	/// <summary>
	/// Repository class for managing orders product.
	/// </summary>

	public class OrderProductRepository : IOrderProductRepository
	{
		private readonly DataContext _context;
		private readonly ICommonMethods _commonMethods;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the OrderProductRepository class.
		/// </summary>
		/// <param name="context">The data context used to interact with the database.</param>
		/// <param name="commonMethods">Common methods used for data operations.</param>
		/// <param name="mapper">An AutoMapper instance for object mapping.</param>
		public OrderProductRepository(DataContext context, ICommonMethods commonMethods, IMapper mapper)
		{
			_context = context;
			_commonMethods = commonMethods;
			_mapper = mapper;
		}

		/// <summary>
		/// Checks if an order product with the specified ID exists in the database.
		/// </summary>
		/// <param name="id">The ID of the order product.</param>
		/// <returns>True if an order product with the given ID exists, otherwise false.</returns>
		public Task<bool> OrderProductExists(int id)
		{
			// Returns a task that represents the asynchronous operation to determine
			// if an order product with the specified ID exists in the database.
			return _context.OrderProducts.AnyAsync(o => o.Id == id);
		}

		/// <summary>
		/// Creates a new order product in the database.
		/// </summary>
		/// <param name="createOrderProduct">The data for creating the new order product.</param>
		/// <returns>True if the order product was created successfully, otherwise false.</returns>
		public async Task<bool> CreateOrderProduct(CreateOrderProductDTO createOrderProduct)
		{
			var newOrderProduct = new OrderProduct
			{
				ProductId = createOrderProduct.ProductId,
				Quantity = createOrderProduct.Quantity
			};

			// Add the new order product entity to the database context
			_context.OrderProducts.Add(newOrderProduct);

			// Save changes to the database and return the result asynchronously
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Deletes an order product from the database by its ID.
		/// </summary>
		/// <param name="id">The ID of the order product to delete.</param>
		/// <returns>True if the order product was deleted successfully, otherwise false.</returns>
		public async Task<bool> DeleteOrderProduct(int id)
		{
			// Check if the order product with the specified ID exists
			if (await OrderProductExists(id))
			{
				// Retrieve the order product entity using the provided ID
				var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(o => o.Id == id);

				// Remove the order product entity from the database context
				_context.OrderProducts.Remove(orderProduct);

				// Save changes to the database and return the result asynchronously
				return await _commonMethods.Save();
			}

			// If the order product does not exist, return false
			return false;
		}

		/// <summary>
		/// Retrieves a list of all order products from the database.
		/// </summary>
		/// <returns>A list of all order products.</returns>
		public async Task<List<OrderProduct>> GetAllOrderProducts()
		{
			// Retrieve all order products from the database asynchronously
			return await _context.OrderProducts.ToListAsync();
		}

		/// <summary>
		/// Retrieves a list of order products associated with the specified user's email.
		/// </summary>
		/// <param name="email">The email of the user.</param>
		/// <returns>A list of order products associated with the user's email.</returns>
		public async Task<List<OrderProduct>> GetOrderProductsByUser(string email)
		{
			// Retrieve order products associated with the user's email from the database asynchronously
			return await _context.OrderProducts
				.Where(op => op.Order.User.Email == email)
				.ToListAsync();
		}

		/// <summary>
		/// Retrieves an order product by its unique identifier (ID).
		/// </summary>
		/// <param name="id">The unique identifier of the order product.</param>
		/// <returns>The retrieved order product or null if not found.</returns>
		public async Task<OrderProduct?> GetOrderProductById(int id)
		{
			// Retrieve the order product with the specified ID from the database asynchronously
			return await _context.OrderProducts.FirstOrDefaultAsync(o => o.Id == id);
		}

		/// <summary>
		/// Updates the properties of an existing order product based on the provided information.
		/// </summary>
		/// <param name="id">The unique identifier of the order product to be updated.</param>
		/// <param name="updateOrderProduct">The DTO containing the updated order product information.</param>
		/// <returns>True if the order product was successfully updated, otherwise false.</returns>
		public async Task<bool> UpdateOrderProduct(int id, UpdateOrderProductDTO updateOrderProduct)
		{
			// Find the order product entity with the specified ID
			var orderProduct = await _context.OrderProducts.FindAsync(id);

			// Check if the order product exists
			if (orderProduct == null)
				return false;

			// Update the quantity property of the order product entity using values from updateOrderProduct DTO
			orderProduct.Quantity = updateOrderProduct.Quantity;

			// Mark the order product entity as updated
			_context.OrderProducts.Update(orderProduct);

			// Save changes to the database using common methods
			return await _commonMethods.Save();
		}
	}
}
