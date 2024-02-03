using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auth.Models
{
    public class Drug
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Usage { get; set; }
        public List<string>? SideEffects { get; set; }
        public string? DosageForm { get; set; }
        public string? Manufacturer { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? Description { get; set; }
    }
}
