using Microsoft.AspNetCore.Authorization.Infrastructure;
using SelXPressApi.Data;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user, int roleId)
        {
            if(user != null)
            {
                return false;
            }
            if(user.Username != null && user.Email != null && user.Password != null && user.Role != null)
            {
                _context.Users.Add(user);
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// select Users.Id, Users.Username, Users.Password, Users.Email, Roles.Name, Roles.Id from Users join Roles on Roles.Id = Users.Id;
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            return _context.Users.Join(_context.Roles, user => user.Id, role => role.Id, (user, role) => new User
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Role = role,
                Carts = null
            }).ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
