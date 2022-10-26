using AutoMapper;
using DailyDolce.Web.Dtos.Product;

namespace DailyDolce.Web.Mapper {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<ProductDto, ProductAndQuantityDto>().ReverseMap();
        }
    }
}
