using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoteTablas.Domain.Entities
{
    public class EntityBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("enabled")]
        public bool Enabled { get; set; } = true;

        [BsonElement("creatorUserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CreatorUserId { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [BsonElement("modifiedAt")]
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("modifierUserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ModifierUserId { get; set; }
    }
}
