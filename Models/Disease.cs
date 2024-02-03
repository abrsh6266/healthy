using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auth.Models
{
    public class Disease
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<string>? Symptoms { get; set; }
        public List<string>? Treatments { get; set; }
        public List<string>? Prevention { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
