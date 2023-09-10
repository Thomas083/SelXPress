using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.CartDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
	/// <summary>
	/// Repository class for managing Carts and their data in the SelXPressApi application.
	/// </summary>
	/// <seealso  cref="Models"/>
	/// <seealso  cref="DTO"/>
	/// <seealso  cref="Controllers"/>
	/// <seealso  cref="Repository"/>
	/// <seealso  cref="Helper"/>
	/// <seealso  cref="DocumentationErrorTemplate"/>
	/// <seealso  cref="Exceptions"/>
	/// <seealso  cref="Interfaces"/>
	/// <seealso  cref="Middleware"/>
	/// <seealso  cref="Data"/>
	public class CartRepository : ICartRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;

		/// <summary>
		/// Initializes a new instance of the CartRepository class.
		/// </summary>
		/// <param name="context">The database context. <see cref="DataContext"/></param>
		/// <param name="commonMethods">Common methods provider. <see cref="ICommonMethods"/></param>
		public CartRepository(DataContext context, ICommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }

		/// <summary>
		/// Retrieves all shopping carts with associated user details.
		/// </summary>
		/// <returns>A list of shopping carts with user information.</returns>
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

		/// <summary>
		/// Retrieves a shopping cart by its ID along with user details.
		/// </summary>
		/// <param name="id">The ID of the cart to retrieve.</param>
		/// <returns>The shopping cart and associated user information.</returns>
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

		/// <summary>
		/// Retrieves all shopping carts for a specific user.
		/// </summary>
		/// <param name="userId">The ID of the user.</param>
		/// <returns>A list of shopping carts for the user.</returns
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

		/// <summary>
		/// Creates a new shopping cart item by an admin.
		/// </summary>
		/// <param name="cartDto">Data for creating the cart item.</param>
		/// <returns>True if the cart item was created successfully; otherwise, false.</returns>
		public async Task<bool> CreateCartByAdmin(CreateCartByAdminDto cartDto)
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

		/// <summary>
		/// Creates a new shopping cart item by a user.
		/// </summary>
		/// <param name="cartDto">Data for creating the cart item.</param>
		/// <param name="email">The email of the user.</param>
		/// <returns>True if the cart item was created successfully; otherwise, false.</returns>
		public async Task<bool> CreateCartByUser(CreateCartDto cartDto, string email)
        {
            var product = await _context.Products.Where(p => p.Id == cartDto.ProductId).FirstAsync();
            var user = await _context.Users.Where(u => u.Email == email).FirstAsync();
            Cart cart = new Cart()
            {
                Product = product,
                ProductId = cartDto.ProductId,
                User = user,
                UserId = user.Id,
                Quantity = cartDto.Quantity
            };
            await _context.Carts.AddAsync(cart);
            return await _commonMethods.Save();
        }

		/// <summary>
		/// Updates the quantity of a shopping cart item.
		/// </summary>
		/// <param name="cartDto">Data for updating the cart item.</param>
		/// <param name="id">The ID of the cart item to update.</param>
		/// <returns>True if the cart item was updated successfully; otherwise, false.</returns>
		public async Task<bool> UpdateCart(UpdateCartDto cartDto, int id)
        {
            if (cartDto != null && cartDto.Quantity != null)
                await _context.Carts.Where(c => c.Id == id)
                    .ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Quantity, x => cartDto.Quantity));
            return await _commonMethods.Save();
        }

		/// <summary>
		/// Deletes a shopping cart item by its ID.
		/// </summary>
		/// <param name="id">The ID of the cart item to delete.</param>
		/// <returns>True if the cart item was deleted successfully; otherwise, false.</returns>
		public async Task<bool> DeleteCart(int id)
        {
            await _context.Carts.Where(c => c.Id == id).ExecuteDeleteAsync();
            return await _commonMethods.Save();
        }

		/// <summary>
		/// Validates the contents of a shopping cart.
		/// </summary>
		/// <returns>True if the cart contents are valid; otherwise, false.</returns>
		public async Task<bool> ValidateCart()
        {
            return await _commonMethods.Save();
        }

		/// <summary>
		/// Checks if a cart with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the cart to check.</param>
		/// <returns>True if a cart with the specified ID exists; otherwise, false.</returns>
		public async Task<bool> CartExists(int id)
        {
            return await _context.Carts.AnyAsync(c => c.Id == id);
        }
    }
}
