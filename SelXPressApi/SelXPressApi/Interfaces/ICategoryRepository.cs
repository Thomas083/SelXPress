using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task<bool> CategoryExists(int id);
        Task<bool> CreateCategory(CreateCategoryDTO createCategory);
        Task<bool> UpdateCategory(UpdateCategoryDTO updateCategory, int id);
        Task<bool> DeleteCategory(int id);
    }
}
