using DailyDolce.Web.Dtos.Product;

namespace DailyDolce.Web.Dtos.Cart {
    public class GetCartDto {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartProductDto> CartProductsDto { get; set; }
        public double TotalOrder { get; set; }
    }
}
