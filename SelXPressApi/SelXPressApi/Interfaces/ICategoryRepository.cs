using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
    }
}
