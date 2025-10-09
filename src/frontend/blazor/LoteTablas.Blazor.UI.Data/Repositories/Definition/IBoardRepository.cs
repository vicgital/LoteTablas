using LoteTablas.Blazor.UI.Models;

namespace LoteTablas.Blazor.UI.Data.Repositories.Definition
{
    public interface IBoardRepository
    {
        Task<Stream> DownloadBoards(List<Board> boards);
    }
}
