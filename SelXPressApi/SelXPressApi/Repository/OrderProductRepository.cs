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
	public class OrderProductRepository : IOrderProductRepository
	{
		private readonly DataContext _context;
		private readonly ICommonMethods _commonMethods;
		private readonly IMapper _mapper;

		public OrderProductRepository(DataContext context, ICommonMethods commonMethods, IMapper mapper)
		{
			_context = context;
			_commonMethods = commonMethods;
			_mapper = mapper;
		}

		/// <summary>
		/// Check if an order product with the specified ID exists.
		/// </summary>
		public Task<bool> OrderProductExists(int id)
		{
			return _context.OrderProducts.AnyAsync(o => o.Id == id);
		}

		/// <summary>
		/// Create a new order product.
		/// </summary>
		public async Task<bool> CreateOrderProduct(CreateOrderProductDTO createOrderProduct)
		{
			var orderProductEntity = _mapper.Map<OrderProduct>(createOrderProduct);
			_context.OrderProducts.Add(orderProductEntity);
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Delete an order product with the specified ID.
		/// </summary>
		public async Task<bool> DeleteOrderProduct(int id)
		{
			if (await OrderProductExists(id))
			{
				var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(o => o.Id == id);
				_context.OrderProducts.Remove(orderProduct);
				return await _commonMethods.Save();
			}
			return false;
		}

		/// <summary>
		/// Get all order products.
		/// </summary>
		public async Task<List<OrderProduct>> GetAllOrderProducts()
		{
			return await _context.OrderProducts.ToListAsync();
		}

		/// <summary>
		/// Get all order products associated with a user's email.
		/// </summary>
		public async Task<List<OrderProduct>> GetOrderProductsByUser(string email)
		{
			return await _context.OrderProducts
				.Where(op => op.Order.User.Email == email)
				.ToListAsync();
		}

		/// <summary>
		/// Get an order product by its ID.
		/// </summary>
		public async Task<OrderProduct?> GetOrderProductById(int id)
		{
			return await _context.OrderProducts.FirstOrDefaultAsync(o => o.Id == id);
		}

		/// <summary>
		/// Update an existing order product.
		/// </summary>
		public async Task<bool> UpdateOrderProduct(int id, UpdateOrderProductDTO updateOrderProduct)
		{
			var orderProduct = await _context.OrderProducts.FindAsync(id);

			if (orderProduct == null)
				return false;

			// Update properties of the orderProduct entity using values from updateOrderProduct DTO
			orderProduct.Quantity = updateOrderProduct.Quantity;

			_context.OrderProducts.Update(orderProduct);

			return await _commonMethods.Save();
		}
	}
}
