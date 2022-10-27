using DailyDolce.Web.Dtos.Product;

namespace DailyDolce.Web.Dtos.Cart {
    public class CartDto {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public double Discount { get; set; }
        public List<CartProductDto> CartProductsDto { get; set; }
        public double TotalOrder { get; set; }
    }
}
