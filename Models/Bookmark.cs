// Bookmark.cs

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auth.Models
{
    public class Bookmark
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public List<Disease>? Diseases { get; set; }
        public List<Drug>? Drugs { get; set; }
        public string? UserId { get; set; }
        public List<Symptom>? Symptoms { get; set; }
    }
}