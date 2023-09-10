using SelXPressApi.DTO.UserDTO;
using SelXPressApi.DTO.OrderDTOProductDTO;

namespace SelXPressApi.DTO.OrderDTO
{
	/// <summary>
	/// Represents a data transfer object (DTO) for an Order. 
	/// Here you can access to model <see cref="Models.Order"/>. 
	/// </summary>
	/// <seealso  cref="Models"/>
	/// <seealso  cref="DTO"/>
	/// <seealso  cref="Controllers"/>
	/// <seealso  cref="Repository"/>
	/// <seealso  cref="Helper"/>
	/// <seealso  cref="DocumentationErrorTemplate"/>
	/// <seealso  cref="Exceptions"/>
	/// <seealso  cref="Interfaces"/>
	/// <seealso  cref="Middleware"/>
	/// <seealso  cref="Data"/>
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
