using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<Role> GetAllRoles();
    }
}
