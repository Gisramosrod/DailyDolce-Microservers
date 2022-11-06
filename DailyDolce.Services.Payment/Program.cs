using DailyDolce.MessageBus;
using DailyDolce.Services.Payment.EventBusConsumer;
using DailyDolce.Services.Payment.Extensions;
using PaymentProcessorSolution;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Payment Processor
builder.Services.AddSingleton<IPaymentProcessor, PaymentProcessor>();

//Message Bus
builder.Services.AddSingleton<IMessageBus, IMessageBus>();

//Message Bus Consumer
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAzureServiceBusConsumer();

app.MapControllers();

app.Run();
