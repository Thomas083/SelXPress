using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Interface for performing CRUD operations on products.
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
	public interface IProductRepository
	{
		/// <summary>
		/// Retrieves a list of products based on specified filters, pagination, and sorting.
		/// </summary>
		/// <param name="categoryName">Name of the category to filter by.</param>
		/// <param name="tagNames">List of tag names to filter by.</param>
		/// <param name="pageNumber">Page number for pagination.</param>
		/// <param name="pageSize">Number of items per page.</param>
		/// <returns>A list of filtered and paginated products.</returns>
		Task<List<Product>> GetAllProductsFilters(string categoryName, List<string> tagNames, int pageNumber, int pageSize);

		/// <summary>
		/// Retrieves a list of all products along with their associated data.
		/// </summary>
		/// <returns>A list of products with associated data.</returns>
		Task<List<AllProductDTO>> GetAllProducts();

		/// <summary>
		/// Retrieves a product by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the product.</param>
		/// <returns>The product with the specified ID.</returns>
		Task<AllProductDTO> GetProductById(int id);

		Task<List<AllProductDTO>> GetProductByUser(string email);

		/// <summary>
		/// Checks if a product with the given ID exists.
		/// </summary>
		/// <param name="id">The unique identifier of the product.</param>
		/// <returns>True if the product exists; otherwise, false.</returns>
		Task<bool> ProductExists(int id);

		/// <summary>
		/// Creates a new product.
		/// </summary>
		/// <param name="createProductDTO">DTO containing the details of the product to create.</param>
		/// <returns>True if the product was created successfully; otherwise, false.</returns>
		Task<bool> CreateProduct(CreateProductDTO createProductDTO, string email);

		/// <summary>
		/// Updates an existing product.
		/// </summary>
		/// <param name="id">The unique identifier of the product to update.</param>
		/// <param name="updateProductDTO">DTO containing the updated details of the product.</param>
		/// <returns>True if the product was updated successfully; otherwise, false.</returns>
		Task<bool> UpdateProduct(int id, UpdateProductDTO updateProductDTO);

		/// <summary>
		/// Deletes a product by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the product to delete.</param>
		/// <returns>True if the product was deleted successfully; otherwise, false.</returns>
		Task<bool> DeleteProduct(int id);
	}
}
