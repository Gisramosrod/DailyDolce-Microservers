using AutoMapper;
using DailyDolce.Services.ShoppingCartApi.Dtos;
using DailyDolce.Services.ShoppingCartApi.Models;

namespace DailyDolce.Services.ShoppingCartApi.Mapper {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<ProductModel, ProductDto>().ReverseMap();
            CreateMap<CartModel, GetCartDto>().ReverseMap();
            CreateMap<CartProductModel, CartProductDto>().ReverseMap();
        }
    }
}
