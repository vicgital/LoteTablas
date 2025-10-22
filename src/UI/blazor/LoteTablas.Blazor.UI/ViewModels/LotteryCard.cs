namespace LoteTablas.Blazor.UI.ViewModels
{
    public class LotteryCard
    {
        public string CardID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int Ordinal { get; set; }
    }
}
