using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<bool> ProductExists(int id);
        Task<Product> GetProductById(int id);
        Task<bool> CreateProduct(CreateProductDTO createProduct);
        Task<bool> UpdateProduct(UpdateProductDTO updateProduct, int id);
        Task<bool> DeleteProduct(int id);
    }
}
