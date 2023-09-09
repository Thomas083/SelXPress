namespace SelXPressApi.DTO.TagDTO
{
	/// <summary>
	/// Data transfer object for a Tag. 
	/// Here you can access to model <see cref="Models.Tag"/>. 
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
	public class TagDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the tag.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ID of the category associated with the tag.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
