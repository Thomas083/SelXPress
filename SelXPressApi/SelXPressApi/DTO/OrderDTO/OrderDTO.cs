using SelXPressApi.DTO.UserDTO;
using SelXPressApi.DTO.OrderDTOProductDTO;

namespace SelXPressApi.DTO.OrderDTO
{
	public class OrderDTO
	{
		/// <summary>
		/// Gets or sets the ID of the order.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the total price of the order.
		/// </summary>
		public float TotalPrice { get; set; }

		/// <summary>
		/// Gets or sets the creation date and time of the order.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Gets or sets the user associated with the order.
		/// </summary>
		public UserDto User { get; set; }

		/// <summary>
		/// Gets or sets the collection of order products for the order.
		/// </summary>
		public ICollection<OrderProductDTO> OrderProducts { get; set; }
	}
}
