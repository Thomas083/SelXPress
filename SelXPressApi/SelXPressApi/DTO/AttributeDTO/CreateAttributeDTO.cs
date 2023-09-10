using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Models;

namespace SelXPressApi.DTO.AttributeDTO
{
	/// <summary>
	/// Data transfer object for creating a new Attribute. 
	/// Here you can access to model <see cref="Models.AttributeData"/>. 
	/// The main DTO is <see cref="AttributeDTO"/>.
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
	public class CreateAttributeDTO
    {
        /// <summary>
        /// Name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the attribute.
        /// </summary>
        public string Type { get; set; }
    }
}
