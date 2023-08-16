using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
    /// <summary>
    /// Interface for interacting with categories.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Get all categories including their associated tags.
        /// </summary>
        /// <returns>List of categories.</returns>
        Task<List<Category>> GetAllCategories();

        /// <summary>
        /// Get a specific category by its ID including its associated tags.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The category with associated tags.</returns>
        Task<Category?> GetCategoryById(int id);

        /// <summary>
        /// Check if a category with a specific ID exists.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>True if the category exists, false otherwise.</returns>
        Task<bool> CategoryExists(int id);

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="createCategory">The data to create the category.</param>
        /// <returns>True if the category was created successfully, false otherwise.</returns>
        Task<bool> CreateCategory(CreateCategoryDTO createCategory);

        /// <summary>
        /// Update a category with new data.
        /// </summary>
        /// <param name="updateCategory">The updated category data.</param>
        /// <param name="id">The ID of the category to update.</param>
        /// <returns>True if the category was updated successfully, false otherwise.</returns>
        Task<bool> UpdateCategory(UpdateCategoryDTO updateCategory, int id);

        /// <summary>
        /// Delete a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>True if the category was deleted successfully, false otherwise.</returns>
        Task<bool> DeleteCategory(int id);
    }
}
