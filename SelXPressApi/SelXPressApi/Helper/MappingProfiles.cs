using AutoMapper;
using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.DTO.TagDTO;
using SelXPressApi.DTO.AttributeDataDTO;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.DTO.CommentDTO;
using SelXPressApi.DTO.CategoryDTO;
using SelXPressApi.Models;
using Attribute = SelXPressApi.Models.Attribute;
using SelXPressApi.DTO.ProductDTO;

namespace SelXPressApi.Helper
{
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
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, CreateProductDTO>();
        }
    }

}
