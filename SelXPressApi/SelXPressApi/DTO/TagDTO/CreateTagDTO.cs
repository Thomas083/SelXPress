namespace SelXPressApi.DTO.TagDTO
{
	/// <summary>
	/// Data transfer object for creating a new Tag. 
	/// Here you can access to model <see cref="Models.Tag"/>. 
	/// The main DTO is <see cref="TagDto"/>.
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
	public class CreateTagDTO
    {
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
