using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	public interface IOrderProductRepository
	{
		Task<bool> OrderProductExists(int id);
		Task<OrderProduct> GetOrderProductById(int id);
		Task<List<OrderProduct>> GetAllOrderProducts();
		Task<bool> CreateOrderProduct(CreateOrderProductDTO createOrderProduct);
		Task<bool> UpdateOrderProduct(int id, UpdateOrderProductDTO updateOrderProduct);
		Task<bool> DeleteOrderProduct(int id);
	}
}
