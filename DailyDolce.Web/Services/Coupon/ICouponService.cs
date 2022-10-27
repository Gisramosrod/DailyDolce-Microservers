using System.Collections.Specialized;

namespace DailyDolce.Web.Services.Coupon {
    public interface ICouponService {
        Task<T> GetCouponByCode<T>(string couponCode, string token = null);
    }
}

