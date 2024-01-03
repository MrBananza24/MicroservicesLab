using InventoryApi.Interfaces;
using InventoryApi.Models;
using InventoryApi.Services;
using ProtoBuf.Grpc.ClientFactory;
using Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<OrdersDatabaseSettings>(
    builder.Configuration.GetSection("OrdersDatabase"));

builder.Services.AddCodeFirstGrpcClient<IInventoryGrpcService>(o =>
{
    o.Address = new Uri(builder.Configuration.GetConnectionString("InventoryApiGrpc"));
})
.ConfigurePrimaryHttpMessageHandler(() =>
    new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>(x =>
{
    return new RabbitMqPublisher(builder.Configuration.GetConnectionString("rabbitmq"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();