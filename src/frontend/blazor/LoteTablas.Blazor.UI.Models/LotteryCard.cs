using System.Text.Json.Serialization;

namespace LoteTablas.Blazor.UI.Models
{
    [Serializable]
    public record LotteryCard
    {
        [JsonPropertyName("cardID")]
        public int CardID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("Description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("imageSmallPath")]
        public string ImageSmallPath { get; set; } = string.Empty;
        [JsonPropertyName("imageMediumPath")]
        public string ImageMediumPath { get; set; } = string.Empty;
        [JsonPropertyName("imageBigPath")]
        public string ImageBigPath { get; set; } = string.Empty;
        [JsonPropertyName("ordinal")]
        public int Ordinal { get; set; }
    }
}
