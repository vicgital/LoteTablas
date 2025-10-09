using System.Text.Json.Serialization;

namespace LoteTablas.Api.Models
{
    public record Board
    {
        public int BoardID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BoardSizeID { get; set; }
        public int LotteryID { get; set; }
        public string FileUrl { get; set; } = string.Empty;

        [JsonIgnore]
        public int OwnerUserID { get; set; }
        public List<BoardCard> BoardCards { get; set; } = new List<BoardCard>();

    }
}
