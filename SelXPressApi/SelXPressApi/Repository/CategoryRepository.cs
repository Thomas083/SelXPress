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

        public Task<bool> CreateCategory(CreateCategoryDTO createCategory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.OrderBy(c => c.Id).ToListAsync();
        }

        public Task<Category?> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategory(UpdateCategoryDTO updateCategory, int id)
        {
            throw new NotImplementedException();
        }
    }
}
