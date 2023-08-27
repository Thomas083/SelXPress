using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.TagDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelXPressApi.Repository
{
	/// <summary>
	/// Repository for managing tags.
	/// </summary>
	public class TagRepository : ITagRepository
	{
		private readonly DataContext _context;
		private readonly ICommonMethods _commonMethods;

		/// <summary>
		/// Initializes a new instance of the <see cref="TagRepository"/> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		/// <param name="commonMethods">Common methods provider.</param>
		public TagRepository(DataContext context, ICommonMethods commonMethods)
		{
			_context = context;
			_commonMethods = commonMethods;
		}

		/// <summary>
		/// Checks if a tag with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the tag to check.</param>
		/// <returns>True if the tag exists; otherwise, false.</returns>
		public async Task<bool> TagExists(int id)
		{
			return await _context.Tags.AnyAsync(t => t.Id == id);
		}

		/// <summary>
		/// Retrieves a list of all tags including their categories.
		/// </summary>
		/// <returns>A list of tags with associated categories.</returns>
		public async Task<List<Tag>> GetAllTags()
		{
			return await _context.Tags.Include(t => t.Category).ToListAsync();
		}

		/// <summary>
		/// Retrieves a tag by its ID including its category.
		/// </summary>
		/// <param name="id">The ID of the tag to retrieve.</param>
		/// <returns>The retrieved tag with associated category, or null if not found.</returns>
		public async Task<Tag?> GetTagById(int id)
		{
			return await _context.Tags.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
		}

		/// <summary>
		/// Creates a new tag.
		/// </summary>
		/// <param name="createTag">DTO containing tag details.</param>
		/// <returns>True if the tag was created successfully; otherwise, false.</returns>
		public async Task<bool> CreateTag(CreateTagDTO createTag)
		{
			Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == createTag.CategoryId);
			if (category == null)
				return false;

			Tag newTag = new Tag
			{
				Name = createTag.Name,
				Category = category
			};
			await _context.Tags.AddAsync(newTag);
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Updates an existing tag's information.
		/// </summary>
		/// <param name="id">The ID of the tag to update.</param>
		/// <param name="updateTag">DTO containing updated tag details.</param>
		/// <returns>True if the tag was updated successfully; otherwise, false.</returns>
		public async Task<bool> UpdateTag(int id, UpdateTagDTO updateTag)
		{
			if (!await TagExists(id))
				return false;

			Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

			if (tag != null && updateTag.Name != null && tag.Name != updateTag.Name)
				tag.Name = updateTag.Name;

			return await _commonMethods.Save();
		}

		/// <summary>
		/// Deletes a tag by its ID.
		/// </summary>
		/// <param name="id">The ID of the tag to delete.</param>
		/// <returns>True if the tag was deleted successfully; otherwise, false.</returns>
		public async Task<bool> DeleteTag(int id)
		{
			if (!await TagExists(id))
				return false;

			Tag tagToDelete = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
			_context.Tags.Remove(tagToDelete);

			return await _commonMethods.Save();
		}
	}
}
