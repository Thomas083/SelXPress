using SelXPressApi.DTO.AttributeDTO;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Interface defining methods to manage attributes and their data.
	/// </summary>
	public interface IAttributeRepository
	{
		/// <summary>
		/// Retrieves a list of all attributes along with their associated data.
		/// </summary>
		/// <returns>A list of attributes with associated data.</returns>
		Task<List<Attribute>> GetAllAttributes();

		/// <summary>
		/// Retrieves an attribute by its ID along with its associated data.
		/// </summary>
		/// <param name="id">The ID of the attribute to retrieve.</param>
		/// <returns>The attribute and its associated data, or null if not found.</returns>
		Task<Attribute?> GetAttributeById(int id);

		/// <summary>
		/// Checks if an attribute with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the attribute to check.</param>
		/// <returns>True if the attribute exists; otherwise, false.</returns>
		Task<bool> AttributeExists(int id);

		/// <summary>
		/// Creates a new attribute using the provided data.
		/// </summary>
		/// <param name="createAttribute">Data to create a new attribute.</param>
		/// <returns>True if the attribute was created successfully; otherwise, false.</returns>
		Task<bool> CreateAttribute(CreateAttributeDTO createAttribute);

		/// <summary>
		/// Updates an existing attribute with the provided data.
		/// </summary>
		/// <param name="id">The ID of the attribute to update.</param>
		/// <param name="updateAttribute">Updated data for the attribute.</param>
		/// <returns>True if the attribute was updated successfully; otherwise, false.</returns>
		Task<bool> UpdateAttribute(int id, UpdateAttributeDTO updateAttribute);

		/// <summary>
		/// Deletes an attribute by its ID if it exists.
		/// </summary>
		/// <param name="id">The ID of the attribute to delete.</param>
		/// <returns>True if the attribute was deleted successfully; otherwise, false.</returns>
		Task<bool> DeleteAttribute(int id);
	}
}
