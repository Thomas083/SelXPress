using SelXPressApi.DTO.TagDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Repository interface for managing tags.
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
	public interface ITagRepository
    {
        /// <summary>
        /// Get all tags with associated categories.
        /// </summary>
        /// <returns>A list of tags.</returns>
        Task<List<Tag>> GetAllTags();

        /// <summary>
        /// Get a tag by its ID with the associated category.
        /// </summary>
        /// <param name="id">The ID of the tag.</param>
        /// <returns>The tag with associated category.</returns>
        Task<Tag> GetTagById(int id);

        /// <summary>
        /// Check if a tag with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the tag.</param>
        /// <returns>True if the tag exists, otherwise false.</returns>
        Task<bool> TagExists(int id);

        /// <summary>
        /// Create a new tag with the given information.
        /// </summary>
        /// <param name="createTag">Data for creating the tag.</param>
        /// <returns>True if the tag was created successfully, otherwise false.</returns>
        Task<bool> CreateTag(CreateTagDTO createTag);

        /// <summary>
        /// Update an existing tag with new information.
        /// </summary>
        /// <param name="id">The ID of the tag to update.</param>
        /// <param name="updateTag">Updated tag data.</param>
        /// <returns>True if the tag was updated successfully, otherwise false.</returns>
        Task<bool> UpdateTag(int id, UpdateTagDTO updateTag);

        /// <summary>
        /// Delete a tag with the given ID.
        /// </summary>
        /// <param name="id">The ID of the tag to delete.</param>
        /// <returns>True if the tag was deleted successfully, otherwise false.</returns>
        Task<bool> DeleteTag(int id);
    }
}
