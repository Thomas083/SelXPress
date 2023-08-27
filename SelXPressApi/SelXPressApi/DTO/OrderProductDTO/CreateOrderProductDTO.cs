namespace SelXPressApi.DTO.OrderDTO
{
	/// <summary>
	/// DTO for creating an order product.
	/// </summary>
	public class CreateOrderProductDTO
	{
		/// <summary>
		/// Gets or sets the ID of the product for the order.
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// Gets or sets the quantity of the product for the order.
		/// </summary>
		public int Quantity { get; set; }
	}
}
