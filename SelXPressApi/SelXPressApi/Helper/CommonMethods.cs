using SelXPressApi.Data;
using SelXPressApi.Interfaces;

namespace SelXPressApi.Helper;
/// <summary>
/// Class to invoke common methods
/// </summary>
public class CommonMethods : ICommonMethods
{
    private readonly DataContext _context;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public CommonMethods(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Method to save the modifications in the database
    /// </summary>
    /// <returns></returns>
    public async Task<bool> Save()
    {
        var saved = await _context.SaveChangesAsync();
        return saved > 0;
    }


}