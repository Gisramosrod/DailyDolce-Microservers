using AutoMapper;
using DailyDolce.Service.OrderApi.Data;
using DailyDolce.Service.OrderApi.EventBusConsumer;
using DailyDolce.Service.OrderApi.Extentions;
using DailyDolce.Service.OrderApi.Mapper;
using DailyDolce.Service.OrderApi.Services;
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
