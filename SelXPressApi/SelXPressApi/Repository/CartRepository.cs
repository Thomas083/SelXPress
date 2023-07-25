using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.CartDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;

        public CartRepository(DataContext context, ICommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }

        public async Task<List<Cart>> GetAllCarts()
        {
            return await _context.Carts.Join(_context.Users, cart => cart.UserId, user => user.Id, (cart, user) =>
                new Cart()
                {
                    Id = cart.Id,
                    Product = cart.Product,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    UserId = user.Id,
                    User = user
                }).ToListAsync();
        }

        public async Task<Cart> GetCartById(int id)
        {
            return await _context.Carts.Where(c => c.Id == id).Join(_context.Users, cart => cart.UserId, user => user.Id, (cart, user) =>
                new Cart()
                {
                    Id = cart.Id,
                    Product = cart.Product,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    UserId = user.Id,
                    User = user
                }).FirstAsync();
        }

        public async Task<List<Cart>> GetCartsByUserId(int userId)
        {
            return await _context.Carts.Where(c => c.UserId == userId).Join(_context.Users, cart => cart.UserId, user => user.Id, (cart, user) =>
                new Cart()
                {
                    Id = cart.Id,
                    Product = cart.Product,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    UserId = user.Id,
                    User = user
                }).ToListAsync();
        }

        public async Task<bool> CreateCart(CreateCartDto cartDto)
        {
            var product = await _context.Products.Where(p => p.Id == cartDto.ProductId).FirstAsync();
            var user = await _context.Users.Where(u => u.Id == cartDto.UserId).FirstAsync();
            Cart cart = new Cart()
            {
                Product = product,
                ProductId = cartDto.ProductId,
                User = user,
                UserId = cartDto.UserId,
                Quantity = cartDto.Quantity
            };
            await _context.Carts.AddAsync(cart);
            return await _commonMethods.Save();
        }

        public async Task<bool> UpdateCart(UpdateCartDto cartDto, int id)
        {
            if (cartDto != null && cartDto.Quantity != null)
                await _context.Carts.Where(c => c.Id == id)
                    .ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Quantity, x => cartDto.Quantity));
            return await _commonMethods.Save();
        }

        public async Task<bool> DeleteCart(int id)
        {
            await _context.Carts.Where(c => c.Id == id).ExecuteDeleteAsync();
            return await _commonMethods.Save();
        }

        public async Task<bool> ValidateCart()
        {
            return await _commonMethods.Save();
        }

        public async Task<bool> CartExists(int id)
        {
            return await _context.Carts.AnyAsync(c => c.Id == id);
        }
    }
}
