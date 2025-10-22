namespace LoteTablas.Blazor.UI.ViewModels
{
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
        public string DoubleCardId { get; set; } = string.Empty;
        public Tuple<int, int> DoubleCardPosition { get; set; } = Tuple.Create<int, int>(0, 0);
        public List<BoardCard> BoardCards { get; set; } = [];
        public bool HasDoubleCard => !string.IsNullOrEmpty(DoubleCardId);
    }
}
