using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Role> GetAllRoles()
        {
            return _context.Roles.OrderBy(r => r.Id).ToList();
        }
    }
}
