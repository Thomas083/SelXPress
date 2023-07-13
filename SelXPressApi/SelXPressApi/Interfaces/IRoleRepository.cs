using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IRoleRepository
    {
        List<Role> GetAllRoles();
        Role GetRoleById(int id);
        void UpdateRoleByID(int id);
        void DeleteRole(int id);
        void CreateRole(Role role);
    }
}
