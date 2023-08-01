using SelXPressApi.DTO.CartDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAllCarts();
        Task<Cart> GetCartById(int id);
        Task<List<Cart>> GetCartsByUserId(int userId);
        Task<bool> CreateCart(CreateCartDto cartDto);
        Task<bool> UpdateCart(UpdateCartDto updateCartDto, int id);
        Task<bool> DeleteCart(int id);
        Task<bool> ValidateCart();
        Task<bool> CartExists(int id);
    }
}
