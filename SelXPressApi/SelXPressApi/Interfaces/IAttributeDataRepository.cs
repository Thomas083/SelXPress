using System.Collections.Generic;
using System.Threading.Tasks;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Interface for managing attribute data operations.
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
	public interface IAttributeDataRepository
	{
		/// <summary>
		/// Retrieves a list of all attribute data records.
		/// </summary>
		/// <returns>A task representing the asynchronous operation that returns a collection of attribute data records.</returns>
		Task<List<AttributeData>> GetAllAttributesData();

		/// <summary>
		/// Retrieves a specific attribute data record by its ID.
		/// </summary>
		/// <param name="id">The ID of the attribute data record.</param>
		/// <returns>A task representing the asynchronous operation that returns the requested attribute data record, or null if not found.</returns>
		Task<AttributeData?> GetAttributeDataById(int id);

		/// <summary>
		/// Checks if an attribute data record with a given ID exists.
		/// </summary>
		/// <param name="id">The ID of the attribute data record.</param>
		/// <returns>A task representing the asynchronous operation that returns true if the attribute data record exists; otherwise, false.</returns>
		Task<bool> AttributeDataExists(int id);

		/// <summary>
		/// Creates a new attribute data record.
		/// </summary>
		/// <param name="createAttribute">The DTO containing the information to create the attribute data record.</param>
		/// <returns>A task representing the asynchronous operation that returns true if the attribute data record was successfully created; otherwise, false.</returns>
		Task<bool> CreateAttributeData(CreateAttributeDataDTO createAttribute);

		/// <summary>
		/// Updates an existing attribute data record.
		/// </summary>
		/// <param name="id">The ID of the attribute data record to update.</param>
		/// <param name="updateAttribute">The DTO containing the updated information for the attribute data record.</param>
		/// <returns>A task representing the asynchronous operation that returns true if the update was successful; otherwise, false.</returns>
		Task<bool> UpdateAttributeData(int id, UpdateAttributeDataDTO updateAttribute);

		/// <summary>
		/// Deletes an attribute data record by its ID.
		/// </summary>
		/// <param name="id">The ID of the attribute data record to delete.</param>
		/// <returns>A task representing the asynchronous operation that returns true if the attribute data record was successfully deleted; otherwise, false.</returns>
		Task<bool> DeleteAttributeData(int id);
	}
}
