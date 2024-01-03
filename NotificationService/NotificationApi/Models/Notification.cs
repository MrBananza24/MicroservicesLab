using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotificationApi.Models;

public class Notification
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("Timestamp")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Date { get; set; }

    [BsonElement("Text")]
    [BsonRepresentation(BsonType.String)]
    public string Description { get; set; } = null!;
}
