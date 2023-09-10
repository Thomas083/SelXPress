using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.Models;

namespace SelXPressApi.DTO.AttributeDTO
{
	/// <summary>
	/// Data Transfer Object for representing an Attribute entity. 
	/// Here you can access to model <see cref="Models.Attribute"/>. 
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
	public class AttributeDTO
    {
        /// <summary>
        /// The ID of the attribute.
        /// </summary>
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
        /// Collection of associated ProductAttribute entities.
        /// </summary>
        public ICollection<ProductAttribute> ProductAttribute { get; set; } = new List<ProductAttribute>();

        /// <summary>
        /// The attribute data associated with the attribute.
        /// </summary>
        public AttributeDataDto AttributeData { get; set; }
    }
}
