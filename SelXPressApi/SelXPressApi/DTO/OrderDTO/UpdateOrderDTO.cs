namespace SelXPressApi.DTO.OrderDTO
{
	/// <summary>
	/// Represents a data transfer object (DTO) for updating an existing Order. 
	/// Here you can access to model <see cref="Models.Order"/>. 
	/// The main DTO is <see cref="OrderDTO"/>.
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
	public class UpdateOrderDTO
	{
		/// <summary>
		/// Gets or sets the updated total price of the order.
		/// </summary>
		public float TotalPrice { get; set; }
	}
}
