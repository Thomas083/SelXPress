using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using System.Threading.Tasks;

namespace SelXPressApi.Helper
{
	/// <summary>
	/// Class to invoke common methods. 
	/// <see cref="ICommonMethods"/>.
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
	public class CommonMethods : ICommonMethods
	{
		private readonly DataContext _context;

		/// <summary>
		/// Constructor for initializing the CommonMethods class.
		/// </summary>
		/// <param name="context">The DataContext instance.</param>
		public CommonMethods(DataContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Method to save the modifications in the database.
		/// </summary>
		/// <returns>A boolean indicating whether the changes were saved successfully.</returns>
		public async Task<bool> Save()
		{
			var saved = await _context.SaveChangesAsync();
			return saved > 0;
		}
	}
}
