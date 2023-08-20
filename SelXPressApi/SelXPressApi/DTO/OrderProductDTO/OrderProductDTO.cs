namespace SelXPressApi.DTO.OrderDTO
{
	public class OrderProductDTO
	{
		/// <summary>
		/// Gets or sets the ID of the order product.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the product associated with the order product.
		/// </summary>
		public ProductDTO.ProductDTO Product { get; set; }

		/// <summary>
		/// Gets or sets the quantity of the product in the order product.
		/// </summary>
		public int Quantity { get; set; }
	}
}
