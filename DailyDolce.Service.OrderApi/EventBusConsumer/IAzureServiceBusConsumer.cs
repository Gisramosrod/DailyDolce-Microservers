namespace DailyDolce.Service.OrderApi.EventBusConsumer {
    public interface IAzureServiceBusConsumer {
        Task Start();
        Task Stop();
    }
}
