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
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;

        public TagRepository(DataContext context, ICommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }

        /// <summary>
        /// Check if a tag with the given ID exists.
        /// </summary>
        public async Task<bool> TagExists(int id)
        {
            return await _context.Tags.AnyAsync(t => t.Id == id);
        }

        /// <summary>
        /// Get all tags with associated categories.
        /// </summary>
        public async Task<List<Tag>> GetAllTags()
        {
            return await _context.Tags.Include(t => t.Category).ToListAsync();
        }

        /// <summary>
        /// Get a tag by its ID with the associated category.
        /// </summary>
        public async Task<Tag?> GetTagById(int id)
        {
            return await _context.Tags.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Create a new tag with the given information.
        /// </summary>
        public async Task<bool> CreateTag(CreateTagDTO createTag)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == createTag.CategoryId);
            if (category == null)
            {
                // Handle the case where the category does not exist
                return false;
            }

            Tag newTag = new Tag
            {
                Name = createTag.Name,
                Category = category
            };
            await _context.Tags.AddAsync(newTag);
            return await _commonMethods.Save();
        }

        /// <summary>
        /// Update an existing tag with new information.
        /// </summary>
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
        /// Delete a tag with the given ID.
        /// </summary>
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
