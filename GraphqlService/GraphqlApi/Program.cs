using GraphqlApi;
using GraphqlApi.Schema;

var builder = WebApplication.CreateBuilder();

builder.Services.AddHttpClient("MyClient").ConfigurePrimaryHttpMessageHandler(o =>
    new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    }
);

builder.Services.AddSingleton(s =>
{
    var client = s.GetRequiredService<IHttpClientFactory>().CreateClient("MyClient");
    return new OrderService(builder.Configuration.GetConnectionString("OrderService"), client);
});

builder.Services.AddSingleton(s =>
{
    var client = s.GetRequiredService<IHttpClientFactory>().CreateClient("MyClient");
    return new ProductService(builder.Configuration.GetConnectionString("ProductService"), client);
});

builder.Services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>();

var app = builder.Build();
app.MapGraphQL();
app.Run();