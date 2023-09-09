using SelXPressApi.DTO.TagDTO;
using System.Collections.Generic;

namespace SelXPressApi.DTO.CategoryDTO
{
	/// <summary>
	/// Data transfer object for representing a Category. 
	/// Here you can access to model <see cref="Models.Category"/>. 
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
	public class CategoryDTO
    {
        /// <summary>
        /// Gets or sets the ID of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of tags associated with the category.
        /// </summary>
        public List<TagDto> Tags { get; set; }
    }
}
