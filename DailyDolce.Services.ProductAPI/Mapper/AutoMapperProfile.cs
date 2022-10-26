using AutoMapper;
using DailyDolce.Services.ProductApi.Dtos;
using DailyDolce.Services.ProductApi.Model;

namespace DailyDolce.Services.ProductApi.Utilities {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
