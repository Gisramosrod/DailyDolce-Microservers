using AutoMapper;
using DailyDolce.Services.CouponApi.Data;
using DailyDolce.Services.CouponApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DailyDolce.Services.CouponApi.Services {
    public class CouponService : ICouponService {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CouponService(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponDto> GetCouponByCode(string couponCode) {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
            return _mapper.Map<CouponDto>(coupon);
        }
    }
}
