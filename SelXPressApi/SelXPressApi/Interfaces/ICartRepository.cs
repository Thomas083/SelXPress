using SelXPressApi.DTO.CartDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Interface defining methods for managing shopping carts and their items.
	/// </summary>
	public interface ICartRepository
	{
		/// <summary>
		/// Retrieves all shopping carts with associated user details.
		/// </summary>
		/// <returns>A list of shopping carts with user information.</returns>
		Task<List<Cart>> GetAllCarts();

		/// <summary>
		/// Retrieves a shopping cart by its ID along with user details.
		/// </summary>
		/// <param name="id">The ID of the cart to retrieve.</param>
		/// <returns>The shopping cart and associated user information.</returns>
		Task<Cart> GetCartById(int id);

		/// <summary>
		/// Retrieves all shopping carts for a specific user.
		/// </summary>
		/// <param name="userId">The ID of the user.</param>
		/// <returns>A list of shopping carts for the user.</returns>
		Task<List<Cart>> GetCartsByUserId(int userId);

		/// <summary>
		/// Creates a new shopping cart item by an admin.
		/// </summary>
		/// <param name="cartDto">Data for creating the cart item.</param>
		/// <returns>True if the cart item was created successfully; otherwise, false.</returns>
		Task<bool> CreateCartByAdmin(CreateCartByAdminDto cartDto);

		/// <summary>
		/// Creates a new shopping cart item by a user.
		/// </summary>
		/// <param name="cartDto">Data for creating the cart item.</param>
		/// <param name="email">The email of the user.</param>
		/// <returns>True if the cart item was created successfully; otherwise, false.</returns>
		Task<bool> CreateCartByUser(CreateCartDto cartDto, string email);

		/// <summary>
		/// Updates the quantity of a shopping cart item.
		/// </summary>
		/// <param name="updateCartDto">Data for updating the cart item.</param>
		/// <param name="id">The ID of the cart item to update.</param>
		/// <returns>True if the cart item was updated successfully; otherwise, false.</returns>
		Task<bool> UpdateCart(UpdateCartDto updateCartDto, int id);

		/// <summary>
		/// Deletes a shopping cart item by its ID.
		/// </summary>
		/// <param name="id">The ID of the cart item to delete.</param>
		/// <returns>True if the cart item was deleted successfully; otherwise, false.</returns>
		Task<bool> DeleteCart(int id);

		/// <summary>
		/// Validates the contents of a shopping cart.
		/// </summary>
		/// <returns>True if the cart contents are valid; otherwise, false.</returns>
		Task<bool> ValidateCart();

		/// <summary>
		/// Checks if a shopping cart with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the cart to check.</param>
		/// <returns>True if a cart with the specified ID exists; otherwise, false.</returns>
		Task<bool> CartExists(int id);
	}
}
