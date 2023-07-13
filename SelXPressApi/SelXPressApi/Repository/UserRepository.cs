using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Reflection.Metadata.Ecma335;

namespace SelXPressApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUser(CreateUserDto user)
        {
            var role = _context.Roles.Where(r => r.Id == user.RoleId).FirstOrDefault();
            if(role != null)
            {
                User newUser = new User
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    Role = role
                };
                await _context.Users.AddAsync(newUser);
                return await Save();
            }
            return false;
        }

        public async Task<bool> DeleteUser(int id)
        {
            if(await UserExists(id))
            {
                await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
                return await Save() ;
            }
            return false;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.Join(_context.Roles, user => user.Role.Id, role => role.Id, (user, role) => new User
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Role = role,
            }).ToListAsync();
        }

        public Task<User?> GetUserById(int id)
        {
            return _context.Users.Where(u => u.Id == id).Join(_context.Roles, user => user.Role.Id, role => role.Id, (user, role) => new User
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Role = role,
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await   _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateUser(UpdateUserDTO updateUser, int id)
        {
            if (!await UserExists(id))
                return false;
            User? user =  _context.Users.Where(u => u.Id == id).FirstOrDefault();

            if (user != null && updateUser.Username != null && user.Username != updateUser.Username)
                await _context.Users.Where(u => u.Id == id).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Username, x => updateUser.Username));

            if (user != null && updateUser.Email != null && user.Email != updateUser.Email)
                await _context.Users.Where(u => u.Id == id).ExecuteUpdateAsync(p2 => p2.SetProperty(x => x.Email, x => updateUser.Email));

            if (user != null && updateUser.Password != null && user.Password != updateUser.Password)
                await _context.Users.Where(u => u.Id == id).ExecuteUpdateAsync(p3 => p3.SetProperty(x => x.Password, x => updateUser.Password));

            return await Save();
        }

        public async Task<bool> UserExists(int id)
        {
            return await _context.Users.AnyAsync(r => r.Id == id);
        }
    }
}
