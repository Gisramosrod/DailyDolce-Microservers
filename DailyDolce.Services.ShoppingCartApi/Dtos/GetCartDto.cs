﻿namespace DailyDolce.Services.ShoppingCartApi.Dtos {
    public class GetCartDto {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public List<CartProductDto> CartProductsDto { get; set; }
    }
}
