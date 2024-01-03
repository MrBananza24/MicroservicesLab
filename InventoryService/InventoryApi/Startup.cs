using InventoryApi.Services;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Server;
using InventoryApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("InventoryDatabase");
builder.Services.AddDbContext<InventoryContext>(o => o.UseNpgsql(connectionString));
builder.Services.AddCodeFirstGrpc();
builder.Services.AddScoped<InventoryService>();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<InventoryService>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();