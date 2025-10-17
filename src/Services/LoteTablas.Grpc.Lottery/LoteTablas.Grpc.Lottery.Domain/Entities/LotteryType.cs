using LoteTablas.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoteTablas.Grpc.Lottery.Domain.Entities
{
    public class LotteryType : EntityBase
    {

        [BsonElement("name")]
        [BsonRequired]
        public string Name { get; set; } = string.Empty;

        [BsonElement("description")]
        [BsonRequired]
        public string Description { get; set; } = string.Empty;

        [BsonElement("code")]
        [BsonRequired]
        public string Code { get; set; } = string.Empty;
    }
}
