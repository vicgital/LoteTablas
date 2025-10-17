using LoteTablas.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoteTablas.Grpc.Lottery.Domain.Entities
{
    public class Lottery : EntityBase
    {

        [BsonElement("name")]
        [BsonRequired]
        public string? Name { get; set; }

        [BsonElement("description")]
        [BsonRequired]
        public string? Description { get; set; }
        
        [BsonElement("lotteryTypeId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? LotteryTypeId { get; set; }
        
        [BsonElement("lotteryType")]
        public string? LotteryType { get; set; }

        [BsonElement("ownerUserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OwnerUserId { get; set; }        

    }
}
