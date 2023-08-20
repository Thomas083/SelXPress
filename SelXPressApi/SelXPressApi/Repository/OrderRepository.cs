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
    /// Repository for managing orders.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        public OrderRepository(DataContext context, ICommonMethods commonMethods, IMapper mapper)
        {
            _context = context;
            _commonMethods = commonMethods;
            _mapper = mapper;
        }

        /// <summary>
        /// Checks if an order with the specified ID exists.
        /// </summary>
        public Task<bool> OrderExists(int id)
        {
            return _context.Orders.AnyAsync(o => o.Id == id);
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        public async Task<bool> CreateOrder(CreateOrderDTO createOrder)
        {
            // Map the DTO to the model
            var orderEntity = _mapper.Map<Order>(createOrder);

            // Set the CreatedAt property
            orderEntity.CreatedAt = DateTime.UtcNow;

            // Add the order entity to the context
            _context.Orders.Add(orderEntity);

            // Save changes to the database
            return await _commonMethods.Save();
        }

        /// <summary>
        /// Deletes an order with the specified ID.
        /// </summary>
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
		/// Retrieves a list of all orders.
		/// </summary>
		public async Task<List<Order>> GetAllOrders()
		{
			var orders = await _context.Orders
				.Include(o => o.User) // Include related User
				.Include(o => o.OrderProducts) // Include related OrderProducts
				.ToListAsync();

			return orders;
		}

		/// <summary>
		/// Retrieves an order by its ID.
		/// </summary>
		public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing order with new information.
        /// </summary>
        public async Task<bool> UpdateOrder(int id, UpdateOrderDTO updateOrder)
        {
            if (!await OrderExists(id)) return false; // Check if order exists

            var order = await _context.Orders.FindAsync(id);

            // Update order properties using values from updateOrder DTO
            order.TotalPrice = updateOrder.TotalPrice;

            // You can add more properties to update as needed

            _context.Orders.Update(order);

            return await _commonMethods.Save(); // Save changes to the database
        }
    }
}
