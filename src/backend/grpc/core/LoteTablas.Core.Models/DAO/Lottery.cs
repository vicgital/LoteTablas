namespace LoteTablas.Core.Models.DAO
{
    public class Lottery
    {
        public int LotteryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LotteryTypeID { get; set; } = string.Empty;
        public string LotteryType { get; set; } = string.Empty;
        public int? OwnerUserID { get; set; }
    }
}
