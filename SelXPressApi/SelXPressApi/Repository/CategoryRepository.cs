using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
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

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.OrderBy(c => c.Id).ToListAsync();
        }
    }
}
