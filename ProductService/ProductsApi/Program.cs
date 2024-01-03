using Microsoft.EntityFrameworkCore;
using ProductService.Models;

var builder = WebApplication.CreateBuilder(args);

//Добавление сервисов
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("ProductDatabase");
builder.Services.AddDbContext<ProductContext>(o => o.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

//Сваггер для отладки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();