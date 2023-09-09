using SelXPressApi.DTO.SellerProductDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces;

/// <summary>
/// Defines the contract for interacting with seller-product-related data in the repository.
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
public interface ISellerProductRepository
{
	/// <summary>
	/// Retrieves a list of all seller-product relationships asynchronously.
	/// </summary>
	Task<List<SellerProduct>> GetAllSellerProduct();

	/// <summary>
	/// Retrieves a seller-product relationship by its unique ID.
	/// </summary>
	/// <param name="id">The ID of the seller-product relationship to retrieve.</param>
	/// <returns>The seller-product relationship with the specified ID.</returns>
	Task<SellerProduct> GetSellerProductById(int id);

	/// <summary>
	/// Retrieves a list of seller-product relationships associated with a user.
	/// </summary>
	/// <param name="userId">The ID of the user to retrieve seller-product relationships for.</param>
	/// <returns>A list of seller-product relationships associated with the specified user.</returns>
	Task<List<SellerProduct>> GetSellerProductByUser(int userId);

	/// <summary>
	/// Retrieves a list of seller-product relationships associated with a product.
	/// </summary>
	/// <param name="productId">The ID of the product to retrieve seller-product relationships for.</param>
	/// <returns>A list of seller-product relationships associated with the specified product.</returns>
	Task<List<SellerProduct>> GetSellerProductByProduct(int productId);

	/// <summary>
	/// Creates a new seller-product relationship using the provided data.
	/// </summary>
	/// <param name="sellerProduct">Data required to create a new seller-product relationship.</param>
	/// <returns>True if the seller-product relationship was created successfully; otherwise, false.</returns>
	Task<bool> CreateSellerProduct(CreateSellerProductDto sellerProduct);

	/// <summary>
	/// Updates a seller-product relationship by its unique ID.
	/// </summary>
	/// <param name="sellerProduct">Updated seller-product relationship data.</param>
	/// <param name="id">The ID of the seller-product relationship to update.</param>
	/// <returns>True if the seller-product relationship was updated successfully; otherwise, false.</returns>
	Task<bool> UpdateSellerProduct(UpdateSellerProductDto sellerProduct, int id);

	/// <summary>
	/// Deletes a seller-product relationship by its unique ID.
	/// </summary>
	/// <param name="id">The ID of the seller-product relationship to delete.</param>
	/// <returns>True if the seller-product relationship was deleted successfully; otherwise, false.</returns>
	Task<bool> DeleteSellerProduct(int id);

	/// <summary>
	/// Checks if a seller-product relationship with the specified ID exists.
	/// </summary>
	/// <param name="id">The ID of the seller-product relationship to check.</param>
	/// <returns>True if the seller-product relationship exists; otherwise, false.</returns>
	Task<bool> SellerProductExists(int id);
}
