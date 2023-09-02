using SelXPressApi.DTO.SellerProductDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces;

public interface ISellerProductRepository
{
    Task<List<SellerProduct>> GetAllSellerProduct();
    Task<SellerProduct> GetSellerProductById(int id);
    Task<List<SellerProduct>> GetSellerProductByUser(int userId);
    Task<List<SellerProduct>> GetSellerProductByProduct(int productId);
    Task<bool> CreateSellerProduct(CreateSellerProductDto sellerProduct);
    Task<bool> UpdateSellerProduct(UpdateSellerProductDto sellerProduct, int id);
    Task<bool> DeleteSellerProduct(int id);
    Task<bool> SellerProductExists(int id);
}