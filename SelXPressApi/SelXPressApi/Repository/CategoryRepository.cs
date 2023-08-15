using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelXPressApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;

        public CategoryRepository(DataContext context, ICommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }

        /// <summary>
        /// Checks if a category with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>True if the category exists, otherwise false.</returns>
        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="category">The category data.</param>
        /// <returns>True if the category was created successfully, otherwise false.</returns>
        public async Task<bool> CreateCategory(CreateCategoryDTO category)
        {
            Category newCategory = new Category
            {
                Name = category.Name
            };
            await _context.Categories.AddAsync(newCategory);
            return await _commonMethods.Save();
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>True if the category was deleted successfully, otherwise false.</returns>
        public async Task<bool> DeleteCategory(int id)
        {
            if (await CategoryExists(id))
            {
                _context.Categories.Remove(new Category { Id = id });
                return await _commonMethods.Save();
            }
            return false;
        }

        /// <summary>
        /// Gets all categories including their associated tags.
        /// </summary>
        /// <returns>A list of categories with their associated tags.</returns>
        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories
                .Include(c => c.Tags) // Eager loading of associated tags
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        /// <summary>
        /// Gets a category by its ID including its associated tags.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category with its associated tags, or null if not found.</returns>
        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories
                .Include(c => c.Tags) // Eager loading of associated tags
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the name of a category.
        /// </summary>
        /// <param name="updateCategory">The updated category data.</param>
        /// <param name="id">The ID of the category to update.</param>
        /// <returns>True if the category was updated successfully, otherwise false.</returns>
        public async Task<bool> UpdateCategory(UpdateCategoryDTO updateCategory, int id)
        {
            if (!await CategoryExists(id))
                return false;

            Category category = await _context.Categories.FindAsync(id);

            if (category != null && updateCategory.Name != null)
                category.Name = updateCategory.Name;

            return await _commonMethods.Save();
        }
    }
}
