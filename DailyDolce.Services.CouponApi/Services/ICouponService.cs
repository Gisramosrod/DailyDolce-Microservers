using DailyDolce.Services.CouponApi.Dtos;

namespace DailyDolce.Services.CouponApi.Services {
    public interface ICouponService {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
