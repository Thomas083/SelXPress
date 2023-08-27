using SelXPressApi.DTO.MarkDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
	public interface IMarkRepository
	{
		/// <summary>
		/// Retrieves a list of all marks asynchronously.
		/// </summary>
		Task<List<Mark>> GetAllMark();

		/// <summary>
		/// Retrieves a mark by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the mark to retrieve.</param>
		/// <returns>The mark with the specified ID.</returns>
		Task<Mark> GetMarkById(int id);

		/// <summary>
		/// Retrieves a list of marks associated with a user.
		/// </summary>
		/// <param name="id">The ID of the user to retrieve marks for.</param>
		/// <returns>A list of marks associated with the specified user.</returns>
		Task<List<Mark>> GetMarkByUser(int id);

		/// <summary>
		/// Retrieves a list of marks associated with a product.
		/// </summary>
		/// <param name="id">The ID of the product to retrieve marks for.</param>
		/// <returns>A list of marks associated with the specified product.</returns>
		Task<List<Mark>> GetMarkByProduct(int id);

		/// <summary>
		/// Creates a new mark using the provided data.
		/// </summary>
		/// <param name="mark">Data required to create a new mark.</param>
		/// <returns>True if the mark was created successfully; otherwise, false.</returns>
		Task<bool> CreateMark(CreateMarkDTO mark);

		/// <summary>
		/// Updates the rate of a mark by its unique ID.
		/// </summary>
		/// <param name="updateMark">Updated mark data.</param>
		/// <param name="id">The ID of the mark to update.</param>
		/// <returns>True if the mark was updated successfully; otherwise, false.</returns>
		Task<bool> UpdateMarkById(UpdateMarkDTO updateMark, int id);

		/// <summary>
		/// Deletes a mark by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the mark to delete.</param>
		/// <returns>True if the mark was deleted successfully; otherwise, false.</returns>
		Task<bool> DeleteMarkById(int id);

		/// <summary>
		/// Checks if a mark with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the mark to check.</param>
		/// <returns>True if the mark exists; otherwise, false.</returns>
		Task<bool> MarkExists(int id);
	}
}
