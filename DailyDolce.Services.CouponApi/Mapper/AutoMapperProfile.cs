using AutoMapper;
using DailyDolce.Services.CouponApi.Dtos;
using DailyDolce.Services.CouponApi.Models;


namespace DailyDolce.Services.CouponApi.Mapper {
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile() {
            CreateMap<CouponModel, CouponDto>().ReverseMap();
        }
    }
}
