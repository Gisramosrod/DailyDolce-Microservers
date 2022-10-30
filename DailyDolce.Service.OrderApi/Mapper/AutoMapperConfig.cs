using AutoMapper;
using DailyDolce.Service.OrderApi.Dtos;
using DailyDolce.Service.OrderApi.Models;

namespace DailyDolce.Service.OrderApi.Mapper {
    public class AutoMapperConfig {
        public static MapperConfiguration RegisterMaps() {

            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<OrderDto, OrderModel>().ReverseMap();
                config.CreateMap<OrderDto, OrderUserModel>().ReverseMap();
                config.CreateMap<OrderProductDto, OrderProductModel>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
