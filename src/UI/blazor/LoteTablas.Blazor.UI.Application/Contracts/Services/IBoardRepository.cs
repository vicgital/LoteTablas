namespace LoteTablas.Blazor.UI.Application.Contracts.Repositories
{
    public interface IBoardRepository
    {
        Task<Stream> DownloadBoards(List<Board> boards);
    }
}
