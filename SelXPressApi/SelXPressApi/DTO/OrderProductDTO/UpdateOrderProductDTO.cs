namespace SelXPressApi.DTO.OrderDTO
{
	/// <summary>
	/// Represents a data transfer object (DTO) for updating an existing Order Product. 
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
	public class UpdateOrderProductDTO
	{
		/// <summary>
		/// Gets or sets the updated quantity of the order product.
		/// </summary>
		public int Quantity { get; set; }
	}
}
