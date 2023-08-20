namespace SelXPressApi.DTO.OrderDTO
{
	public class CreateOrderDTO
	{
		public float TotalPrice { get; set; }
		public DateTime CreatedAt { get; set; }
		public int UserId { get; set; }
		public List<CreateOrderProductDTO> OrderProducts { get; set; }
	}
}
