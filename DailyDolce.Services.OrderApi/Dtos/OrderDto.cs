namespace DailyDolce.Services.OrderApi.Dtos {
    public class OrderDto {
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVC { get; set; }
        public string ExpirationDate { get; set; }
        public double TotalOrder { get; set; }

        public List<OrderProductDto> CartProductsDto { get; set; }
    }
}
