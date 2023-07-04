﻿using SelXPressApi.Models;

namespace SelXPressApi.DTO.UserDTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

    }
}
