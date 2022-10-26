using DailyDolce.Web.Dtos.Product;

namespace DailyDolce.Web.Dtos.Cart {
    public class CartProductDto {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
