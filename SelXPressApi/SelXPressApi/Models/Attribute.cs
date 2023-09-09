using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SelXPressApi.Models
{
	/// <summary>
	/// Model of the Attribute table. 
    /// <see cref="DTO.AttributeDTO"/>
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
	public class Attribute
    {
        /// <summary>
        /// The ID of the attribute.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the attribute.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The date and time when the attribute was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the attribute was last modified.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// List of ProductAttribute objects for the Many-to-Many relationship.
        /// </summary>
        public ICollection<ProductAttribute> ProductAttributes { get; set; }

        /// <summary>
        /// Collection of AttributeData objects associated with the attribute.
        /// </summary>
        public ICollection<AttributeData> AttributeData { get; set; }
    }
}
