using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelXPressApi.Repository
{
	/// <summary>
	/// Repository class for managing Orders.
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
	public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;
        private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderRepository"/> class.
		/// </summary>
		/// <param name="context">The database context. <see cref="DataContext"/></param>
		/// <param name="commonMethods">Common methods provider. <see cref="ICommonMethods"/></param>
		/// <param name="mapper">Automapper instance. <see cref="IMapper"/></param>
		public OrderRepository(DataContext context, ICommonMethods commonMethods, IMapper mapper)
		{
			_context = context;
			_commonMethods = commonMethods;
			_mapper = mapper;
		}

		/// <summary>
		/// Checks if an order with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the order.</param>
		/// <returns>True if the order exists, otherwise false.</returns>
		public Task<bool> OrderExists(int id)
		{
			return _context.Orders.AnyAsync(o => o.Id == id);
		}

		/// <summary>
		/// Creates a new order based on the provided order details.
		/// </summary>
		/// <param name="createOrder">The DTO containing order information to be used for creating the order.</param>
		/// <returns>True if the order creation was successful, otherwise false.</returns>
		public async Task<bool> CreateOrder(CreateOrderDTO createOrder)
		{
			float totalPrice = 0;
			List<Cart> cartList = await _context.Carts.Where(c => c.UserId == createOrder.UserId).Include(p => p.Product).ToListAsync();
			
			var user = await _context.Users.Where(u => u.Id == createOrder.UserId).FirstAsync();
			
			var newOrder = new Order
			{
				User = user,
				TotalPrice = totalPrice,
				CreatedAt = DateTime.Now,
				OrderProducts = new List<OrderProduct>()
			};

			for (int i = 0; i < cartList.Count; i++)
			{
				var orderProductToAdd = new OrderProduct()
				{
					Order = newOrder,
					OrderId = newOrder.Id,
					Product = cartList[i].Product,
					ProductId = cartList[i].ProductId,
					Quantity = cartList[i].Quantity
				};
				totalPrice = totalPrice + orderProductToAdd.Product.Price;
				if (orderProductToAdd.Quantity > 1)
				{
					for (int j = 1; j < orderProductToAdd.Quantity; j++)
					{
						totalPrice = totalPrice + orderProductToAdd.Product.Price;
					}
				}
				newOrder.OrderProducts.Add(orderProductToAdd);
			}
			newOrder.TotalPrice = totalPrice;
			_context.Orders.Add(newOrder);
			await _context.Carts.Where(c => c.UserId == createOrder.UserId).ExecuteDeleteAsync();
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Deletes an order with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the order to be deleted.</param>
		/// <returns>True if the order was successfully deleted, otherwise false.</returns>
		public async Task<bool> DeleteOrder(int id)
		{
			if (await OrderExists(id))
			{
				var order = await _context.Orders.FindAsync(id);
				_context.Orders.Remove(order);
				return await _commonMethods.Save();
			}

			return false;
		}

		/// <summary>
		/// Retrieves a list of all orders with associated user and order product information.
		/// </summary>
		/// <returns>A list of orders with their associated user and order product details.</returns>
		public async Task<List<Order>> GetAllOrders()
		{
			var orders = await _context.Orders
				.Include(o => o.User)
				.Include(o => o.OrderProducts)
				.ThenInclude(op => op.Product)
				.ToListAsync();

			return orders;
		}

		/// <summary>
		/// Retrieves an order by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the order.</param>
		/// <returns>The order with the specified identifier, or null if not found.</returns>
		public async Task<Order> GetOrderById(int id)
		{
			return await _context.Orders.Where(o => o.Id == id)
				.Include(o => o.User)
				.Include(o => o.OrderProducts)
				.ThenInclude(op => op.Product)
				.FirstAsync();
		}

		/// <summary>
		/// Updates an existing order with new information.
		/// </summary>
		/// <param name="id">The unique identifier of the order to be updated.</param>
		/// <param name="updateOrder">The DTO containing updated order information.</param>
		/// <returns>True if the order was successfully updated, false otherwise.</returns>
		public async Task<bool> UpdateOrder(int id, UpdateOrderDTO updateOrder)
		{
			// Check if the order with the specified ID exists
			if (!await OrderExists(id))
				return false;

			// Retrieve the existing order from the database
			var order = await _context.Orders.FindAsync(id);

			// Update the total price of the order using the value from the updateOrder DTO
			order.TotalPrice = updateOrder.TotalPrice;

			// Update the order entity in the database
			_context.Orders.Update(order);

			// Save changes to the database and return the result
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Retrieves a list of orders for a user based on their email address.
		/// </summary>
		/// <param name="email">The email address of the user for whom to retrieve orders.</param>
		/// <returns>A list of orders associated with the specified user's email address, or null if the user does not exist.</returns>
		public async Task<List<Order>> GetOrderByUser(string email)
		{
			// Check if a user with the specified email address exists in the database
			if (await _context.Users.Where(u => u.Email == email).AnyAsync())
			{
				// Retrieve the user with the specified email address
				var user = await _context.Users.Where(u => u.Email == email).FirstAsync();
				// Retrieve and return the list of orders associated with the user
				var orders = await _context.Orders.Where(o => o.User == user)
					.Include(o => o.User)
					.Include(o => o.OrderProducts)
					.ThenInclude(op => op.Product)
					.ToListAsync();
				return orders;
			}

			return null; // Return null if the user does not exist in the database
		}

	}
}
