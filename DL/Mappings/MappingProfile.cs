using AutoMapper;
using Data_Layer.DTOs;
using Data_Layer.Models;

namespace Data_Layer.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TradesCoreUser, UserDto>();
            CreateMap<UserDto, TradesCoreUser>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
