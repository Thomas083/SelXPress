using SelXPressApi.DTO.ProductAttributeDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IProductAttributeRepository
    {
        Task<List<ProductAttribute>> GetAllProductAttributes();
        Task<ProductAttribute?> GetProductAttributeById(int id);
        Task<bool> ProductAttributeExists(int id);
        Task<bool> CreateProductAttribute(CreateProductAttributeDTO createProductAttribute);
        Task<bool> UpdateProductAttribute(int id, UpdateProductAttributeDTO updateProductAttribute);
        Task<bool> DeleteProductAttribute(int id);
    }
}
