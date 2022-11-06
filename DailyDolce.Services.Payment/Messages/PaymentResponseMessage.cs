using DailyDolce.MessageBus;

namespace DailyDolce.Services.Payment.Messages {
    public class PaymentResponseMessage: BaseMessage {
        public int OrderId { get; set; }
        public bool Status { get; set; }
    }
}
