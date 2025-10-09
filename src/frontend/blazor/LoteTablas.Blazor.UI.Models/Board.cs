
namespace LoteTablas.Blazor.UI.Models
{
    [Serializable]
    public class Board
    {
        public Board()
        {
        }

        public Board(int boardId)
        {
            BoardId = boardId;
        }

        public int BoardId { get; set; } = 0;
        public int DoubleCardId { get; set; } = 0;
        public Tuple<int, int> DoubleCardPosition { get; set; } = Tuple.Create<int, int>(0, 0);
        public List<BoardCard> BoardCards { get; set; } = [];
        public bool HasDoubleCard => DoubleCardId > 0;

    }
}
