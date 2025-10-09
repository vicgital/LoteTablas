namespace LoteTablas.Core.Models
{
    public class BoardLite
    {

        public int BoardID { get; set; }
        public List<BoardCardLite> BoardCards { get; set; } = [];
    }
}
