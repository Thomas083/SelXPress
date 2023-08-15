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
        private ICommonMethods _commonMethods;

        public TagRepository(DataContext context, ICommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }

        public async Task<bool> TagExists(int id)
        {
            return await _context.Tags.AnyAsync(t => t.Id == id);
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _context.Tags.Include(t => t.Category).ToListAsync();
        }

        public async Task<Tag?> GetTagById(int id)
        {
            return await _context.Tags.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> CreateTag(CreateTagDTO createTag)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == createTag.CategoryId);
            if (category == null)
            {
                // Gérer le cas où la catégorie n'existe pas
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

        public async Task<bool> UpdateTag(int id, UpdateTagDTO updateTag)
        {
            if (!await TagExists(id))
                return false;

            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (tag != null && updateTag.Name != null && tag.Name != updateTag.Name)
                tag.Name = updateTag.Name;

            return await _commonMethods.Save();
        }

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
