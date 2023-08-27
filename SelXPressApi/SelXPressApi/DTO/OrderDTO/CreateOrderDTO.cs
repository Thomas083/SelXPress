namespace SelXPressApi.DTO.OrderDTO
{
	public class CreateOrderDTO
	{
		/// <summary>
		/// Gets or sets the total price of the order.
		/// </summary>
		public float TotalPrice { get; set; }

		/// <summary>
		/// Gets or sets the creation date and time of the order.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Gets or sets the user ID associated with the order.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Gets or sets the list of order products for the order.
		/// </summary>
		public List<CreateOrderProductDTO> OrderProducts { get; set; }
	}
}
