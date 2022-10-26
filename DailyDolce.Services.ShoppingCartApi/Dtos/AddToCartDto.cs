namespace DailyDolce.Services.ShoppingCartApi.Dtos {
    public class AddToCartDto {
        public string UserId { get; set; }
        public ProductDto ProductDto { get; set; }
        public int Quantity { get; set; }
    }
}
