using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

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

        public async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.Where(c => c.Id == id).AnyAsync();
        }

        public async Task<bool> CreateCategory(CreateCategoryDTO category)
        {
            Category newCategory = new Category
            {
                Name = category.Name
            };
            await _context.Categories.AddAsync(newCategory);
            return await _commonMethods.Save();
        }

        public async Task<bool> DeleteCategory(int id)
        {
            if(await CategoryExists(id))
            {
                await _context.Categories.Where(r => r.Id == id).ExecuteDeleteAsync();
                return await _commonMethods.Save();
            }
            return false;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.Where(c => c.Id == id).FirstAsync();
        }

        public async Task<bool> UpdateCategory(UpdateCategoryDTO updateCategory, int id)
        {
            if(!await CategoryExists(id))
                return false;
            Category category = await _context.Categories.Where(c => c.Id == id).FirstAsync();

            if(category != null && updateCategory.Name != null)
                await _context.Categories.Where(c => c.Id == id)
                    .ExecuteUpdateAsync(c1 => c1.SetProperty(x => x.Name, updateCategory.Name));
            return await _commonMethods.Save();

        }
    }
}
