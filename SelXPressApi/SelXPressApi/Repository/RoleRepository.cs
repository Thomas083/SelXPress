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

        public void CreateRole(Role role)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(int id)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetAllRoles()
        {
           return _context.Roles.OrderBy(r => r.Id).ToList();
        }

        public Role GetRoleById(int id)
        {
            return _context.Roles.Where(r => r.Id == id).FirstOrDefault();
        }

        public void UpdateRoleByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
