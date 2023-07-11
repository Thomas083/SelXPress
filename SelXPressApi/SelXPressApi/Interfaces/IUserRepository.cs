using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAllUsers();
    }
}
