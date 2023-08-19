using SelXPressApi.DTO.ProductAttributeDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    /// <summary>
    /// Interface for managing product attribute data.
    /// </summary>
    public interface IProductAttributeRepository
    {
        /// <summary>
        /// Get all product attributes.
        /// </summary>
        /// <returns>A list of all product attributes.</returns>
        Task<List<ProductAttribute>> GetAllProductAttributes();

        /// <summary>
        /// Get a product attribute by its ID.
        /// </summary>
        /// <param name="id">The ID of the product attribute.</param>
        /// <returns>The product attribute with the given ID, if found; otherwise, null.</returns>
        Task<ProductAttribute?> GetProductAttributeById(int id);

        /// <summary>
        /// Check if a product attribute with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the product attribute.</param>
        /// <returns>True if the product attribute exists; otherwise, false.</returns>
        Task<bool> ProductAttributeExists(int id);

        /// <summary>
        /// Create a new product attribute.
        /// </summary>
        /// <param name="createProductAttribute">Data for creating a new product attribute.</param>
        /// <returns>True if the product attribute was successfully created; otherwise, false.</returns>
        Task<bool> CreateProductAttribute(CreateProductAttributeDTO createProductAttribute);

        /// <summary>
        /// Update an existing product attribute.
        /// </summary>
        /// <param name="id">The ID of the product attribute to update.</param>
        /// <param name="updateProductAttribute">Updated data for the product attribute.</param>
        /// <returns>True if the product attribute was successfully updated; otherwise, false.</returns>
        Task<bool> UpdateProductAttribute(int id, UpdateProductAttributeDTO updateProductAttribute);

        /// <summary>
        /// Delete a product attribute by its ID.
        /// </summary>
        /// <param name="id">The ID of the product attribute to delete.</param>
        /// <returns>True if the product attribute was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteProductAttribute(int id);
    }
}
