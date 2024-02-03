using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auth.Models
{
    public class Symptom
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public List<string>? Treatment { get; set; }
        public string? Duration { get; set; }
        public List<string>? Prevention { get; set; }
    }
}
