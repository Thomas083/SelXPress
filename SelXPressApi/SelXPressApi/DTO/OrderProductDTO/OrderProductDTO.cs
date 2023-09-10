namespace SelXPressApi.DTO.OrderDTOProductDTO
{
	/// <summary>
	/// Represents a data transfer object (DTO) for an Order Product.
	/// Here you can access to model <see cref="Models.OrderProduct"/>. 
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
