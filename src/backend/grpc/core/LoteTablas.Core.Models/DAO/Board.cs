namespace LoteTablas.Core.Models.DAO
{
    public class Board
    {
        public int BoardID { get; set; }
        public string? Name { get; set; }
        public int BoardSizeID { get; set; }
        public int LotteryID { get; set; }
        public string? FileUrl { get; set; }
        public int OwnerUserID { get; set; }
    }
}
