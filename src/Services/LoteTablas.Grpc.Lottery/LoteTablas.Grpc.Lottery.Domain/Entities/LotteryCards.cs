using LoteTablas.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoteTablas.Grpc.Lottery.Domain.Entities
{
    public class LotteryCards : EntityBase
    {
        [BsonElement("lotteryId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string LotteryId { get; set; } = string.Empty;

        [BsonElement("cards")]
        [BsonRequired]
        public List<LotteryCard> Cards { get; set; } = [];
    }

    public class LotteryCard 
    {
        [BsonElement("cardId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string CardId { get; set; } = string.Empty;

        [BsonElement("ordinal")]
        [BsonRequired]
        public int Ordinal { get; set; }
    } 
}
