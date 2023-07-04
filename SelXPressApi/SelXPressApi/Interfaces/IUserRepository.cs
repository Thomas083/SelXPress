using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetAllUsers();
        User? GetUserById(int id);
        bool UserExists(int id);

        bool CreateUser(User user, int roleId);
        bool Save();
    }
}
