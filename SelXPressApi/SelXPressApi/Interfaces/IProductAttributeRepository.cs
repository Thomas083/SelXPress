using SelXPressApi.DTO.ProductAttributeDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Interface for managing product attribute data.
	/// </summary>
	public interface IProductAttributeRepository
	{
		/// <summary>
		/// Retrieves a list of all product attributes.
		/// </summary>
		/// <returns>A list of all product attributes.</returns>
		Task<List<ProductAttribute>> GetAllProductAttributes();

		/// <summary>
		/// Retrieves a product attribute by its ID.
		/// </summary>
		/// <param name="id">The ID of the product attribute to retrieve.</param>
		/// <returns>The retrieved product attribute, or null if not found.</returns>
		Task<ProductAttribute?> GetProductAttributeById(int id);

		/// <summary>
		/// Checks if a product attribute with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the product attribute to check.</param>
		/// <returns><c>true</c> if the product attribute exists; otherwise, <c>false</c>.</returns>
		Task<bool> ProductAttributeExists(int id);

		/// <summary>
		/// Creates a new product attribute.
		/// </summary>
		/// <param name="createProductAttribute">The data to create the product attribute.</param>
		/// <returns><c>true</c> if the product attribute is successfully created; otherwise, <c>false</c>.</returns>
		Task<bool> CreateProductAttribute(CreateProductAttributeDTO createProductAttribute);

		/// <summary>
		/// Updates an existing product attribute with new information.
		/// </summary>
		/// <param name="id">The ID of the product attribute to update.</param>
		/// <param name="updateProductAttribute">Updated information for the product attribute.</param>
		/// <returns><c>true</c> if the update was successful; otherwise, <c>false</c>.</returns>
		Task<bool> UpdateProductAttribute(int id, UpdateProductAttributeDTO updateProductAttribute);

		/// <summary>
		/// Deletes a product attribute with the specified ID.
		/// </summary>
		/// <param name="id">The ID of the product attribute to be deleted.</param>
		/// <returns><c>true</c> if the product attribute is successfully deleted; <c>false</c> if the specified ID doesn't exist.</returns>
		Task<bool> DeleteProductAttribute(int id);
	}
}
