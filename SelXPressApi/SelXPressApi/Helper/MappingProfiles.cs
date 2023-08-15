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
    /// <summary>
    /// AutoMapper configuration class for mappings between entities and DTOs.
    /// </summary>
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>(); // Mapping from User to UserDto
            CreateMap<User, CreateUserDto>(); // Mapping from User to CreateUserDto
            CreateMap<Comment, CommentDTO>(); // Mapping from Comment to CommentDTO
            CreateMap<Category, CategoryDTO>(); // Mapping from Category to CategoryDTO
            CreateMap<Attribute, AttributeDTO>(); // Mapping from Attribute to AttributeDTO
            CreateMap<AttributeData, AttributeDataDto>(); // Mapping from AttributeData to AttributeDataDto
            CreateMap<CreateAttributeDataDTO, AttributeData>(); // Mapping from CreateAttributeDataDTO to AttributeData
        }
    }
}
