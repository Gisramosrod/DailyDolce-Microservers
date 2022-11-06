using AutoMapper;
using DailyDolce.Services.OrderApi.Dtos;
using DailyDolce.Services.OrderApi.Models;

namespace DailyDolce.Services.OrderApi.Mapper {
    public class AutoMapperConfig {
        public static MapperConfiguration RegisterMaps() {

            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<OrderDto, OrderModel>().ReverseMap();
                config.CreateMap<OrderDto, OrderUserModel>().ReverseMap();
                config.CreateMap<OrderProductDto, OrderProductModel>().ReverseMap();
                config.CreateMap<OrderDto, PaymentRequestDto>().ReverseMap();

            });

            return mappingConfig;
        }
    }
}
