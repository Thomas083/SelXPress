using Microsoft.EntityFrameworkCore;
using SelXPressApi.Models;

namespace SelXPressApi.Contexts;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        
    }

    public DbSet<UserModel> UserModels { get; set; } = null;
}