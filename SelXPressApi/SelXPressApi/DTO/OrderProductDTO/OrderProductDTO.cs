namespace SelXPressApi.DTO.OrderDTO
{
	public class OrderProductDTO
	{
		public int Id { get; set; }
		public ProductDTO.ProductDTO Product { get; set; }
		public int Quantity { get; set; }
	}
}
