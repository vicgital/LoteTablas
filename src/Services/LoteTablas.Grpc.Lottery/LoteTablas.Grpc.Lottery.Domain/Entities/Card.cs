using LoteTablas.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoteTablas.Grpc.Lottery.Domain.Entities
{
    public class Card : EntityBase
    {
        [BsonElement("name")]
        [BsonRequired]
        public string? Name { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("imagePath")]
        [BsonRequired]
        public string? ImagePath { get; set; }

        
    }
}
