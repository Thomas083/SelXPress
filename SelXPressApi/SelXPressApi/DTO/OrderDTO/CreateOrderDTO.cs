using SelXPressApi.DTO.OrderDTOProductDTO;
using SelXPressApi.Models;

namespace SelXPressApi.DTO.OrderDTO
{
	public class CreateOrderDTO
	{
		/// <summary>
		/// Gets or sets the total price of the order.
		/// </summary>
		public float TotalPrice { get; set; }
        
		/// <summary>
		/// Gets or sets the user ID associated with the order.
		/// </summary>
		public User User { get; set; }

		/// <summary>
		/// Gets or sets the list of order products for the order.
		/// </summary>
		public List<OrderProduct> OrderProducts { get; set; }
	}
}
