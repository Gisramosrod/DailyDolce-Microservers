using DailyDolce.Service.OrderApi.EventBusConsumer;

namespace DailyDolce.Service.OrderApi.Extentions {
    public static class AplicationBuilderExtension {
        public static IAzureServiceBusConsumer ServiceBusConsumer { get; set; }

        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app) {
            ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopped.Register(OnStop);
            return app;
        }

        private static void OnStart() {
            ServiceBusConsumer.Start();
        }

        private static void OnStop() {
            ServiceBusConsumer.Stop();
        }
    }
}
