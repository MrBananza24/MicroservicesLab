using InventoryApi.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace InventoryApi.Services;

public class RabbitMqPublisher : IDisposable, IRabbitMqPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqPublisher(string hostName)
    {
        var factory = new ConnectionFactory() { HostName = hostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "MyQueue",
               durable: false,
               exclusive: false,
               autoDelete: false,
               arguments: null);
    }
    
    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }

    public void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "",
                        routingKey: "MyQueue",
                        basicProperties: null,
                        body: body);
    }
}
