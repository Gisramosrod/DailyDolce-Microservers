using DailyDolce.MessageBus;

namespace DailyDolce.Services.ShoppingCartApi.Dtos {
    public class OrderDto : BaseMessage {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVC { get; set; }
        public string ExpirationDate { get; set; }
        public CartDto CartDto { get; set; }
    }
}
