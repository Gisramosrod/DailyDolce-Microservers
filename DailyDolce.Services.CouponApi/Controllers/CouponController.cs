using DailyDolce.Services.CouponApi.Dtos;
using DailyDolce.Services.CouponApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailyDolce.Services.CouponApi.Controllers {
    [Route("api/coupon")]
    [ApiController]
    public class CouponController : ControllerBase {
        private readonly ICouponService _couponService;
        protected ResponseDto _response;

        public CouponController(ICouponService couponService) {
            _couponService = couponService;
            _response = new();
        }

        [HttpGet("{couponCode}")]
        public async Task<ActionResult<ResponseDto>> GetCouponByCode(string couponCode) {
            try {
                var coupon = await _couponService.GetCouponByCode(couponCode);
                if (coupon == null) {
                    _response.Success = false;
                    _response.ErrorMessages = new List<string> { "There is no coupon with that code." };
                }
                _response.Data = coupon;

            }catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages = new List<string> {ex.Message};
            }
            return _response;       
        }
    }
}
