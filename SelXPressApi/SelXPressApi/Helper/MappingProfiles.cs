using AutoMapper;
using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, CreateUserDto>();
            CreateMap<Category, CategoryDTO>();
        }
    }
}
