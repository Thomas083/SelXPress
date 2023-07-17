using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        bool ProductExists(int id);
        Product GetProductById(int id);
    }
}
