using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

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
		public Task<bool> OrderProductExists(int id)
		{
			return _context.OrderProducts.AnyAsync(o => o.Id == id);
		}

		public async Task<bool> CreateOrderProduct(CreateOrderProductDTO createOrderProduct)
		{
			var orderProductEntity = _mapper.Map<OrderProduct>(createOrderProduct);

			_context.OrderProducts.Add(orderProductEntity);

			return await _commonMethods.Save();
		}

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

		public async Task<List<OrderProduct>> GetAllOrderProducts()
		{
			return await _context.OrderProducts.ToListAsync();
		}

		public async Task<List<OrderProduct>> GetOrderProductsByUser(string email)
		{
			return await _context.OrderProducts
				.Where(op => op.Order.User.Email == email)
				.ToListAsync();
		}

		public async Task<OrderProduct?> GetOrderProductById(int id)
		{
			return await _context.OrderProducts.FirstOrDefaultAsync(o => o.Id == id);
		}


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
