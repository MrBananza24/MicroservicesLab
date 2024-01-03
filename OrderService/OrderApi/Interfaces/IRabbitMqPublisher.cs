namespace InventoryApi.Interfaces;

public interface IRabbitMqPublisher
{
    void SendMessage(string message);
}
