namespace LoteTablas.Blazor.UI.Models;

[Serializable]
public class DownloadBoardsRequest
{
    public List<UserBoard> Boards { get; set; } = [];
}

[Serializable]
public class UserBoard
{
    public int BoardID { get; set; }
    public List<BoardCard> BoardCards { get; set; } = [];
}
