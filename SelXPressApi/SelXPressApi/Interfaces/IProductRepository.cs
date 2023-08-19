using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<bool> ProductExists(int id);
        Task<bool> CreateProduct(CreateProductDTO createProductDTO);
        Task<bool> UpdateProduct(int id, UpdateProductDTO updateProductDTO);
        Task<bool> DeleteProduct(int id);
    }
}
