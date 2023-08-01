using AutoMapper;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.DTO.CategoryDTO;
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
            CreateMap<Comment, CommentDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Attribute, AttributeDTO>();
            CreateMap<AttributeData, AttributeDataDTO>()
                .ForMember(dest => dest.Attribute, opt => opt.MapFrom(src => src.Attribute));
        }
    }

}
