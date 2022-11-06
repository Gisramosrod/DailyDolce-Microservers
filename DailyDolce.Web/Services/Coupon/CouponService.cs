using DailyDolce.Web.Models;
using DailyDolce.Web.Services.Base;

namespace DailyDolce.Web.Services.Coupon {
    public class CouponService : BaseService, ICouponService {

        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory) {

        }

        public async Task<T> GetCouponByCode<T>(string couponCode, string token = null) {
            return await SendRequest<T>(new ApiRequest() {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.GatewayBaseUrl}/api/coupon/{couponCode}",
                AccessToken = token
            });
        }
    }
}
