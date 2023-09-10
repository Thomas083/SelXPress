namespace SelXPressApi.DTO.TagDTO
{
	/// <summary>
	/// Data Transfer Object for updating a Tag. 
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
	public class UpdateTagDTO
    {
        /// <summary>
        /// New name for the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the new category for the tag.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
