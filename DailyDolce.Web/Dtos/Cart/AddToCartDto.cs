using DailyDolce.Web.Dtos.Product;

namespace DailyDolce.Web.Dtos.Cart {
    public class AddToCartDto {
        public string UserId { get; set; }
        public ProductDto ProductDto { get; set; }
        public int Quantity { get; set; }

    }
}
