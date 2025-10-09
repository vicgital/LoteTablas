namespace LoteTablas.Grpc.Lottery.Domain.Entities
{
    public class LotteryCard
    {
        public int CardID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageSmallPath { get; set; } = string.Empty;
        public string ImageMediumPath { get; set; } = string.Empty;
        public string ImageBigPath { get; set; } = string.Empty;
        public int Ordinal { get; set; }
    }
}
