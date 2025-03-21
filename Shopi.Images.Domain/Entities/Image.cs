using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shopi.Images.Domain.Entities;

public class Image
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string FileName { get; set; }
    public string Url { get; set; }
    
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid ProductId { get; set; }
}