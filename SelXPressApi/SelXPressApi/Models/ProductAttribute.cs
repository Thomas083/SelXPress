using Microsoft.EntityFrameworkCore;

namespace SelXPressApi.Models
{
	/// <summary>
	/// Represents a model for the ProductAttribute table. 
	/// <see cref="DTO.ProductAttributeDTO"/>
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
	public class ProductAttribute
    {
        /// <summary>
        /// Gets or sets the ID of the ProductAttribute.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Product object associated with the ProductAttribute.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated Product object.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the Attribute object associated with the ProductAttribute.
        /// </summary>
        public Attribute Attribute { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated Attribute object.
        /// </summary>
        public int AttributeId { get; set; }
    }
}
