using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
    /// <summary>
    /// Interface for performing CRUD operations on products.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Get a list of products based on filters, pagination, and sorting.
        /// </summary>
        /// <param name="categoryName">Filter products by category name.</param>
        /// <param name="tagNames">Filter products by tag names.</param>
        /// <param name="pageNumber">Page number for pagination.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>List of filtered and paginated products.</returns>
        Task<List<Product>> GetAllProducts(string categoryName, List<string> tagNames, int pageNumber, int pageSize);

        /// <summary>
        /// Get a product by its unique identifier.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>The product with the specified ID.</returns>
        Task<Product> GetProductById(int id);

        /// <summary>
        /// Check if a product with the given ID exists.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>True if the product exists, false otherwise.</returns>
        Task<bool> ProductExists(int id);

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="createProductDTO">DTO containing product details.</param>
        /// <returns>True if the product was created successfully, false otherwise.</returns>
        Task<bool> CreateProduct(CreateProductDTO createProductDTO);

        /// <summary>
        /// Update an existing product.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <param name="updateProductDTO">DTO containing updated product details.</param>
        /// <returns>True if the product was updated successfully, false otherwise.</returns>
        Task<bool> UpdateProduct(int id, UpdateProductDTO updateProductDTO);

        /// <summary>
        /// Delete a product by its unique identifier.
        /// </summary>
        /// <param name="id">Product ID.</param>
        /// <returns>True if the product was deleted successfully, false otherwise.</returns>
        Task<bool> DeleteProduct(int id);
    }
}
