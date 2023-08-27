using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
	public class CommentRepository : ICommentRepository
	{
		private readonly DataContext _context;
		private readonly ICommonMethods _commonMethods;

		public CommentRepository(DataContext context, ICommonMethods commonMethods)
		{
			_context = context;
			_commonMethods = commonMethods;
		}

		/// <summary>
		/// Retrieves a list of all comments along with associated marks, users, and products.
		/// </summary>
		public async Task<List<Comment>> GetAllComments()
		{
			return await _context.Comments.Join(_context.Marks, comment => comment.Mark.Id, mark => mark.Id,
				(comment, mark) => new Comment()
				{
					Id = comment.Id,
					Message = comment.Message,
					Title = comment.Title,
					CreatedAt = comment.CreatedAt,
					Mark = mark,
					User = comment.User,
					Product = comment.Product
				}).ToListAsync();
		}

		/// <summary>
		/// Retrieves a comment by its unique ID along with its associated mark, user, and product.
		/// </summary>
		/// <param name="id">The ID of the comment to retrieve.</param>
		/// <returns>The comment and its associated data.</returns>
		public async Task<Comment> GetCommentById(int id)
		{
			return await _context.Comments.Where(c => c.Id == id).Join(_context.Marks, comment => comment.Mark.Id, mark => mark.Id,
				(comment, mark) => new Comment()
				{
					Id = comment.Id,
					Message = comment.Message,
					Title = comment.Title,
					CreatedAt = comment.CreatedAt,
					Mark = mark,
					User = comment.User,
					Product = comment.Product
				}).FirstAsync();
		}

		/// <summary>
		/// Retrieves a list of comments for a specific product, along with their associated marks, users, and products.
		/// </summary>
		/// <param name="productId">The ID of the product for which to retrieve comments.</param>
		/// <returns>A list of comments related to the specified product.</returns>
		public async Task<List<Comment>> GetCommentByProduct(int productId)
		{
			return await _context.Comments.Where(c => c.Product.Id == productId).Join(_context.Marks, comment => comment.Mark.Id, mark => mark.Id,
				(comment, mark) => new Comment()
				{
					Id = comment.Id,
					Message = comment.Message,
					Title = comment.Title,
					CreatedAt = comment.CreatedAt,
					Mark = mark,
					User = comment.User,
					Product = comment.Product
				}).ToListAsync();
		}

		/// <summary>
		/// Retrieves a list of comments for a specific user, along with their associated marks, users, and products.
		/// </summary>
		/// <param name="userId">The ID of the user for which to retrieve comments.</param>
		/// <returns>A list of comments related to the specified user.</returns>
		public async Task<List<Comment>> GetCommentByUser(int userId)
		{
			return await _context.Comments.Where(c => c.User.Id == userId).Join(_context.Marks, comment => comment.Mark.Id, mark => mark.Id,
				(comment, mark) => new Comment()
				{
					Id = comment.Id,
					Message = comment.Message,
					Title = comment.Title,
					CreatedAt = comment.CreatedAt,
					Mark = mark,
					User = comment.User,
					Product = comment.Product
				}).ToListAsync();
		}

		/// <summary>
		/// Creates a new comment using the provided data, including associated user, product, and mark.
		/// </summary>
		/// <param name="commentDto">Data required to create a new comment.</param>
		/// <returns>True if the comment was created successfully; otherwise, false.</returns>
		public async Task<bool> CreateComment(CreateCommentDTO commentDto)
		{
			var user = await _context.Users.Where(u => u.Id == commentDto.UserId).FirstAsync();
			var product = await _context.Products.Where(p => p.Id == commentDto.ProductId).FirstAsync();
			Mark mark = new Mark()
			{
				rate = commentDto.Rate
			};
			await _context.Marks.AddAsync(mark);
			await _commonMethods.Save();
			Comment comment = new Comment()
			{
				Message = commentDto.Message,
				Title = commentDto.Title,
				CreatedAt = DateTime.Now,
				Mark = mark,
				Product = product,
				User = user
			};
			await _context.Comments.AddAsync(comment);

			return await _commonMethods.Save();
		}

		/// <summary>
		/// Updates the message and title of a comment by its unique ID.
		/// </summary>
		/// <param name="updateComment">Updated comment data.</param>
		/// <param name="id">The ID of the comment to update.</param>
		/// <returns>True if the comment was updated successfully; otherwise, false.</returns>
		public async Task<bool> UpdateCommentById(UpdateCommentDTO updateComment, int id)
		{
			var comment = _context.Comments.Where(c => c.Id == id).FirstAsync();
			if (updateComment.Message != null)
			{
				await _context.Comments.Where(c => c.Id == id)
					.ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Message, x => updateComment.Message));
			}
			if (updateComment.Title != null)
				await _context.Comments.Where(c => c.Id == id)
					.ExecuteUpdateAsync(p2 => p2.SetProperty(x => x.Title, x => updateComment.Title));
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Deletes a comment by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the comment to delete.</param>
		/// <returns>True if the comment was deleted successfully; otherwise, false.</returns>
		public async Task<bool> DeleteCommentById(int id)
		{
			await _context.Comments.Where(c => c.Id == id).ExecuteDeleteAsync();
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Checks if a comment with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the comment to check.</param>
		/// <returns>True if the comment exists; otherwise, false.</returns>
		public async Task<bool> CommentExists(int id)
		{
			return await _context.Comments.Where(c => c.Id == id).AnyAsync();
		}
	}
}
