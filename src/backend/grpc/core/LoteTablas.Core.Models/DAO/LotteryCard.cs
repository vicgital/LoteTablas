namespace LoteTablas.Core.Models.DAO
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
