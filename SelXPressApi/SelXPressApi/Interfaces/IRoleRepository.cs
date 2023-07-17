using SelXPressApi.DTO.RoleDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<bool> UpdateRoleByID(int id, UpdateRoleDTO updateRole);
        Task<bool> DeleteRole(int id);
        Task<bool> CreateRole(CreateRoleDTO role);
        Task<bool> RoleExists(int id);
    }
}
