using InventoryApi.Services;
using NotificationApi.Models;
using NotificationApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<NotificationDatabaseSettings>(
    builder.Configuration.GetSection("NotificationsDatabase"));
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddHostedService<RabbitMqListener>();
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
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