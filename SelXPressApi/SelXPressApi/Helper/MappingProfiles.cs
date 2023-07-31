<<<<<<< HEAD
﻿using AutoMapper;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.DTO.AttributeDTO;
=======
using AutoMapper;
using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.DTO.CommentDTO;
>>>>>>> develop
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Models;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, CreateUserDto>();
<<<<<<< HEAD
            CreateMap<Attribute, AttributeDTO>();
            CreateMap<AttributeData, AttributeDataDTO>();
=======
            CreateMap<Comment, CommentDTO>();
            CreateMap<Category, CategoryDTO>();
>>>>>>> develop
        }
    }
}
