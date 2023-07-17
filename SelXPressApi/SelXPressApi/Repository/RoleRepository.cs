using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.RoleDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        private readonly CommonMethods _commonMethods;

        public RoleRepository(DataContext context, CommonMethods commonMethods)
        {
            _context = context;
            _commonMethods = commonMethods;
        }

        public async Task<bool> CreateRole(CreateRoleDTO role)
        {
            Role newRole = new Role
            {
                Name = role.RoleName
            };
            await _context.Roles.AddAsync(newRole);
            return await _commonMethods.Save();
        }

        public async Task<bool> DeleteRole(int id)
        {
            if (await RoleExists(id))
            {
                await _context.Roles.Where(r => r.Id == id).ExecuteDeleteAsync();
                return await _commonMethods.Save();
            }
            return false;
        }

        public async Task<List<Role>> GetAllRoles()
        {
           return await _context.Roles.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _context.Roles.Where(r => r.Id == id).FirstAsync();
        }

        public async Task<bool> UpdateRoleByID(int id, UpdateRoleDTO updateRole)
        {
            if (!await RoleExists(id))
                return false;
            Role role = await _context.Roles.Where(r => r.Id == id).FirstAsync();

            if (role != null && updateRole.RoleName != null)
                await _context.Roles.Where(r => r.Id == id)
                    .ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Name, x => updateRole.RoleName));
            return await _commonMethods.Save();
        }
        public async Task<bool> RoleExists(int id)
        {
            return await _context.Roles.Where(r => r.Id == id).AnyAsync();
        }
    }
}
