using Microsoft.AspNetCore.Mvc;
using SelXPressApi.Models;

namespace SelXPressApi.DTO.AttributeDTO
{
	/// <summary>
	/// Data transfer object for updating an Attribute. 
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
	public class UpdateAttributeDTO
    {
        /// <summary>
        /// Updated name of the attribute.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Updated type of the attribute.
        /// </summary>
        public string Type { get; set; }
    }
}
