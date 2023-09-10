namespace SelXPressApi.DTO.OrderDTO
{
	/// <summary>
	/// DTO for creating an Order Product.
	/// Here you can access to model <see cref="Models.OrderProduct"/>. 
	/// The main DTO is <see cref="OrderDTOProductDTO.OrderProductDTO"/>.
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
