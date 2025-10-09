namespace LoteTablas.Core.Models
{
    public class Board
    {
        public int BoardID { get; set; }
        public string? Name { get; set; }
        public int BoardSizeID { get; set; }
        public int LotteryID { get; set; }
        public string? FileUrl { get; set; }
        public int OwnerUserID { get; set; }
        public List<BoardCard> BoardCards { get; set; } = [];
    }
}
