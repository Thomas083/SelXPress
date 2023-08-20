using SelXPressApi.DTO.UserDTO;

namespace SelXPressApi.DTO.OrderDTO
{
    public class OrderDTO
	{
		public int Id { get; set; }
		public float TotalPrice { get; set; }
		public DateTime CreatedAt { get; set; }
		public UserDto User { get; set; }
		public ICollection<OrderProductDTO> OrderProducts { get; set; }
	}
}
