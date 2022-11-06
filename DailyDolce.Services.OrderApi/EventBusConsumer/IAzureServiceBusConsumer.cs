namespace DailyDolce.Services.OrderApi.EventBusConsumer {
    public interface IAzureServiceBusConsumer {
        Task Start();
        Task Stop();
    }
}
