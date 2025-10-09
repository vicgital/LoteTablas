using LoteTablas.Core.Models;

namespace LoteTablas.Core.Business.Interfaces
{
    public interface IBoardComponent
    {
        Task<bool> DeleteBoard(int boardID);
        Task<byte[]> GetBoardDocument(int boardId);
        Task<byte[]> GetBoardsDocument(List<int> boardIds);
        Task<byte[]> GetUserBoardsLiteDocument(List<BoardLite> userBoards);
        Task<List<Models.Board>> GetBoardsByUserID(int userID);
        Task<List<Models.BoardSize>> GetBoardSizes();
        Task<Models.Board?> GetFullBoardByBoardID(int boardID);
        Task<Models.Board?> SaveBoard(Models.Board board);
        Task<bool> SaveBoardCards(int boardID, List<Models.BoardCard> boardCards);
    }
}
