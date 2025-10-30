namespace LoteTablas.Api.Domain
{
    public class LotteryCard
    {
        public string CardId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int Ordinal { get; set; } 
    }
}
