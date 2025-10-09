namespace LoteTablas.Core.Models
{
    public class Lottery
    {

        public int LotteryID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LotteryTypeID { get; set; }
        public string LotteryType { get; set; } = string.Empty;
        public int? OwnerUserID { get; set; }

    }
}
