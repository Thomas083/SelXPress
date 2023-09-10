namespace SelXPressApi.DTO.CategoryDTO
{
	/// <summary>
	/// Data transfer object for updating a Category. 
	/// Here you can access to model <see cref="Models.Category"/>. 
	/// The main DTO is <see cref="CategoryDTO"/>
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
	public class UpdateCategoryDTO
    {
        /// <summary>
        /// Gets or sets the updated name of the category.
        /// </summary>
        public string Name { get; set; }
    }
}
