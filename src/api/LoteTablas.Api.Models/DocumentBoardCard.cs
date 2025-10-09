namespace LoteTablas.Api.Models
{
    public record DocumentBoardCard
    {
        public int Ordinal { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
