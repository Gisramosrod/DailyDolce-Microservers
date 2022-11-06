using AutoMapper;
using DailyDolce.MessageBus;
using DailyDolce.Services.OrderApi.Data;
using DailyDolce.Services.OrderApi.EventBusConsumer;
using DailyDolce.Services.OrderApi.Extensions;
using DailyDolce.Services.OrderApi.Mapper;
using DailyDolce.Services.OrderApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Db
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfiguration"));
});

//Mapper
IMapper mapper = AutoMapperConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Order Service
var optionBuilder = new DbContextOptionsBuilder<DataContext>();
optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfiguration"));
builder.Services.AddSingleton(new OrderService(optionBuilder.Options, mapper));

//Azure Service Bus Consumer
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

//Message Bus
builder.Services.AddSingleton<IMessageBus, AzureServiceBusMessageBus>();

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
