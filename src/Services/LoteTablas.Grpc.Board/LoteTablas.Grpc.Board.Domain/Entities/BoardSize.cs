using LoteTablas.Domain.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace LoteTablas.Grpc.Board.Domain.Entities
{
    public class BoardSize : EntityBase
    {
        [BsonElement("code")]
        [BsonRequired]
        public string? Code { get; set; }

        [BsonElement("xSize")]
        [BsonRequired]
        public int XSize { get; set; }

        [BsonElement("ySize")]
        [BsonRequired]
        public int YSize { get; set; }
    }
}
