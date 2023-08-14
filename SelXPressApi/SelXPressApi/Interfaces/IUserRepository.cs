using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<bool> UserExists(int id);
        Task<bool> CreateUser(CreateUserDto createUser);
        Task<bool> UpdateUser(UpdateUserDTO updateUser, string email);
        Task<User> GetUserByEmail(string email);
        Task<bool> UserExistsEmail(string email);
        Task<bool> DeleteUser(int id);
    }
}
