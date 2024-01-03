using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotificationApi.Models;

namespace NotificationApi.Services;

public class NotificationService
{
    readonly IMongoCollection<Notification> collection;

    public NotificationService(IOptions<NotificationDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        collection = mongoDatabase.GetCollection<Notification>(options.Value.CollectionName);
    }

    public async Task<List<Notification>> GetAsync() =>
        await collection.Find(_ => true).ToListAsync();

    public async Task<Notification?> GetAsync(string id) =>
        await collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(string description)
    {
        var notification = new Notification
        {
            Description = description,
            Date = DateTime.UtcNow
        };
        await collection.InsertOneAsync(notification);
    }
}