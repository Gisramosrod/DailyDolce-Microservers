namespace DailyDolce.Services.Payment.EventBusConsumer {
    public interface IAzureServiceBusConsumer {
        Task Start();
        Task Stop();
    }
}
