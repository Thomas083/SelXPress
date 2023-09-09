namespace SelXPressApi.DTO.ProductAttributeDTO
{
	/// <summary>
	/// Data transfer object for creating a new Product Attribute. 
	/// Here you can access to model <see cref="Models.ProductAttribute"/>. 
	/// The main DTO is <see cref="ProductAttributeDTO"/>.
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
	public class CreateProductAttributeDTO
    {
        /// <summary>
        /// Gets or sets the ID of the product associated with the attribute.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the attribute.
        /// </summary>
        public int AttributeId { get; set; }
    }
}
