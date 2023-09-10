using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.MarkDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
	/// <summary>
	/// Repository class for managing Marks and their data in the SelXPressApi application.
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
	public class MarkRepository : IMarkRepository
	{
		private readonly DataContext _context;
		private readonly ICommonMethods _commonMethods;

		/// <summary>
		/// Initializes a new instance of the MarkRepository class.
		/// </summary>
		/// <param name="context">The database context. <see cref="DataContext"/></param>
		/// <param name="commonMethods">Common methods provider. <see cref="ICommonMethods"/></param>
		public MarkRepository(DataContext context, ICommonMethods commonMethods)
		{
			_context = context;
			_commonMethods = commonMethods;
		}

		/// <summary>
		/// Retrieves a collection of all marks.
		/// </summary>
		/// <returns>A collection of all marks.</returns>
		public ICollection<Mark> GetAllMarks()
		{
			return _context.Marks.OrderBy(m => m.Id).ToList();
		}

		/// <summary>
		/// Retrieves a list of all marks asynchronously.
		/// </summary>
		/// <returns>A list of all marks.</returns>
		public async Task<List<Mark>> GetAllMark()
		{
			return await _context.Marks.OrderBy(m => m.Id).ToListAsync();
		}

		/// <summary>
		/// Retrieves a mark by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the mark to retrieve.</param>
		/// <returns>The mark with the specified ID.</returns>
		public async Task<Mark> GetMarkById(int id)
		{
			return await _context.Marks.Where(m => m.Id == id).FirstAsync();
		}

		/// <summary>
		/// Retrieves a list of marks associated with a user.
		/// </summary>
		/// <param name="id">The ID of the user to retrieve marks for.</param>
		/// <returns>A list of marks associated with the specified user.</returns>
		public async Task<List<Mark>> GetMarkByUser(int id)
		{
			return await _context.Comments.Where(c => c.User.Id == id).Join(_context.Marks, comment => comment.Mark.Id, mark => mark.Id,
				(comment, mark) => new Mark()
				{
					Id = mark.Id,
					rate = mark.rate
				}).ToListAsync();
		}

		/// <summary>
		/// Retrieves a list of marks associated with a product.
		/// </summary>
		/// <param name="id">The ID of the product to retrieve marks for.</param>
		/// <returns>A list of marks associated with the specified product.</returns>
		public async Task<List<Mark>> GetMarkByProduct(int id)
		{
			return await _context.Comments.Where(c => c.Product.Id == id).Join(_context.Marks, comm => comm.Mark.Id,
				mark => mark.Id, (comm, mark) => new Mark()
				{
					Id = mark.Id,
					rate = mark.rate
				}).ToListAsync();
		}

		/// <summary>
		/// Creates a new mark using the provided data.
		/// </summary>
		/// <param name="mark">Data required to create a new mark.</param>
		/// <returns>True if the mark was created successfully; otherwise, false.</returns>
		public async Task<bool> CreateMark(CreateMarkDTO mark)
		{
			Mark newMark = new Mark()
			{
				rate = mark.Rate
			};
			await _context.Marks.AddAsync(newMark);
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Updates the rate of a mark by its unique ID.
		/// </summary>
		/// <param name="updateMark">Updated mark data.</param>
		/// <param name="id">The ID of the mark to update.</param>
		/// <returns>True if the mark was updated successfully; otherwise, false.</returns>
		public async Task<bool> UpdateMarkById(UpdateMarkDTO updateMark, int id)
		{
			if (!await MarkExists(id))
				return false;
			var mark = await _context.Marks.Where(m => m.Id == id).FirstAsync();
			if (updateMark.Rate != mark.rate)
			{
				await _context.Marks.Where(m => m.Id == id)
					.ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.rate, x => updateMark.Rate));
			}
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Deletes a mark by its unique ID.
		/// </summary>
		/// <param name="id">The ID of the mark to delete.</param>
		/// <returns>True if the mark was deleted successfully; otherwise, false.</returns>
		public async Task<bool> DeleteMarkById(int id)
		{
			if (!await MarkExists(id))
				return false;
			await _context.Marks.Where(m => m.Id == id).ExecuteDeleteAsync();
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Checks if a mark with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the mark to check.</param>
		/// <returns>True if the mark exists; otherwise, false.</returns>
		public async Task<bool> MarkExists(int id)
		{
			return await _context.Marks.Where(m => m.Id == id).AnyAsync();
		}
	}
}
