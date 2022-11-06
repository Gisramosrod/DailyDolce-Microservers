using DailyDolce.MessageBus;

namespace DailyDolce.Services.OrderApi.Dtos {
    public class PaymentRequestDto : BaseMessage {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public string CVC { get; set; }
        public string ExpirationDate { get; set; }
    }
}
