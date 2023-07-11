using AutoMapper;
using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Models;

namespace SelXPressApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, CreateProductDTO>();
        }
    }
}
