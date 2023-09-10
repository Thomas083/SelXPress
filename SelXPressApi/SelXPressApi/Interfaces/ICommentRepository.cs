using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Provides an interface for managing comments and their associated data.
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
	public interface ICommentRepository
	{
		/// <summary>
		/// Retrieves a list of all comments along with their associated data.
		/// </summary>
		Task<List<Comment>> GetAllComments();

		/// <summary>
		/// Retrieves a comment by its unique ID along with its associated data.
		/// </summary>
		/// <param name="id">The ID of the comment to retrieve.</param>
		/// <returns>The comment and its associated data.</returns>
		Task<Comment> GetCommentById(int id);

		/// <summary>
		/// Retrieves a list of comments for a specific product along with their associated data.
		/// </summary>
		/// <param name="productId">The ID of the product for which to retrieve comments.</param>
		/// <returns>A list of comments related to the specified product.</returns>
		Task<List<Comment>> GetCommentByProduct(int productId);

		/// <summary>
		/// Retrieves a list of comments for a specific user along with their associated data.
		/// </summary>
		/// <param name="userId">The ID of the user for which to retrieve comments.</param>
		/// <returns>A list of comments related to the specified user.</returns>
		Task<List<Comment>> GetCommentByUser(int userId);

		/// <summary>
		/// Creates a new comment using the provided data.
		/// </summary>
		/// <param name="comment">Data required to create a new comment.</param>
		/// <returns>True if the comment was created successfully; otherwise, false.</returns>
		Task<bool> CreateComment(CreateCommentDTO comment);

		/// <summary>
		/// Updates the message and title of a comment by its unique ID.
		/// </summary>
		/// <param name="updateComment">Updated comment data.</param>
		/// <param name="id">The ID of the comment to update.</param>
		/// <returns>True if the comment was updated successfully; otherwise, false.</returns>
		Task<bool> UpdateCommentById(UpdateCommentDTO updateComment, int id);

		/// <summary>
		/// Deletes a comment by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the comment to delete.</param>
		/// <returns>True if the comment was deleted successfully; otherwise, false.</returns>
		Task<bool> DeleteCommentById(int id);

		/// <summary>
		/// Checks if a comment with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the comment to check.</param>
		/// <returns>True if the comment exists; otherwise, false.</returns>
		Task<bool> CommentExists(int id);
	}
}
