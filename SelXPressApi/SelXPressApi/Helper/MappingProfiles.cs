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
using SelXPressApi.DTO.ProductAttributeDTO;
using SelXPressApi.DTO.OrderDTO;
using SelXPressApi.DTO.OrderDTOProductDTO;

namespace SelXPressApi.Helper
{
	/// <summary>
	/// AutoMapper profile for mapping between model and DTO classes.
	/// </summary>
	/// <seealso  cref="Models"/>
	/// <seealso  cref="DTO"/>
	/// <seealso  cref="Controllers"/>
	/// <seealso  cref="Repository"/>
	/// <seealso  cref="Helper"/>
	/// <seealso  cref="DocumentationErrorTemplate"/>
	/// <seealso  cref="Exceptions"/>
	/// <seealso  cref="Interfaces"/>
	/// <seealso  cref="Middleware"/>
	/// <seealso  cref="Data"/>
	public class MappingProfiles : Profile
    {
        /// <summary>
        /// Constructor for MappingProfiles class.
        /// </summary>
        public MappingProfiles()
        {
            // Mapping User model to UserDto
            CreateMap<User, UserDto>();

            // Mapping User model to CreateUserDto
            CreateMap<User, CreateUserDto>();

            // Mapping Comment model to CommentDTO
            CreateMap<Comment, CommentDTO>();

            // Mapping Category model to CategoryDTO and including Tags
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            // Mapping Tag model to TagDto
            CreateMap<Tag, TagDto>();

            // Mapping Attribute model to AttributeDTO
            CreateMap<Attribute, AttributeDTO>();

            // Mapping AttributeData model to AttributeDataDto
            CreateMap<AttributeData, AttributeDataDto>();

            // Mapping AttributeData model to CreateAttributeDataDTO
            CreateMap<AttributeData, CreateAttributeDataDTO>();

            // Mapping Product model to ProductDTO
            CreateMap<Product, ProductDTO>();

            // Mapping Product model to AllProductDTO
            CreateMap<Product, AllProductDTO>();

            // Mapping Product model to CreateProductDTO
            CreateMap<Product, CreateProductDTO>();

            // Mapping ProductAttribute model to ProductAttributeDTO
            CreateMap<ProductAttribute, ProductAttributeDTO>();

            // Mapping Order model to OrderDTO
            CreateMap<Order, OrderDTO>();

            // Mapping OrderProduct model to OrderProductDTO
            CreateMap<OrderProduct, OrderProductDTO>();

            // Mapping UpdateProductDTO to Product
            CreateMap<UpdateProductDTO, Product>();
        }
    }
}
