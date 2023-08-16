using AutoMapper;
using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.DTO.TagDTO;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Models;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Helper
{
    /// <summary>
    /// AutoMapper configuration class for mappings between entities and DTOs.
    /// </summary>
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, CreateUserDto>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags)); // Map Category model to CategoryDTO and include Tags
            CreateMap<Tag, TagDto>(); // Map Tag model to TagDto
            CreateMap<Attribute, AttributeDTO>();
            CreateMap<AttributeData, AttributeDataDto>();
        }
    }

}
