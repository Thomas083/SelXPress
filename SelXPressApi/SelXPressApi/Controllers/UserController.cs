using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Contexts;
using SelXPressApi.Models;

namespace SelXPressApi.Controllers;

[Route("api/users")]
[ApiController]
public class UserController
{

    public UserContext _context;
    
    [HttpGet]
    public async Task<ActionResult<List<UserModel>>> GetAllUsers()
    {
        List<UserModel> users = await _context.UserModels.ToListAsync();
        return users;
    }

    [HttpPost]
    public async Task<ActionResult<UserModel>> CreateUser(UserModel userModel)
    {
        _context.UserModels.
    }
}