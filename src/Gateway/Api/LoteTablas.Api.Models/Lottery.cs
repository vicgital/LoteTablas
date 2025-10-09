using System.Text.Json.Serialization;

namespace LoteTablas.Api.Models
{
    public record Lottery
    {
        public int LotteryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LotteryTypeID { get; set; }
        public string LotteryType { get; set; } = string.Empty;

        [JsonIgnore]
        public int OwnerUserID { get; set; }
    }
}
