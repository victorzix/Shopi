using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shopi.Images.API.Models;

public class Image
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string FileName { get; set; }
    public string Url { get; set; }
    public string ProductId { get; set; }
}