using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAllCategories();
    }
}
