using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAllProducts();
    }
}
