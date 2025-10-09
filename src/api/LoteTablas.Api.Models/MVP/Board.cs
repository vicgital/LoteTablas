namespace LoteTablas.Api.Models.MVP
{
    public record Board
    {
        public int BoardID { get; set; }
        public List<BoardCard> BoardCards { get; set; } = [];
    }
}
