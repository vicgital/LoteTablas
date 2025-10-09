using LoteTablas.Api.Models;

namespace LoteTablas.Api.Business.Components.Definition
{
    public interface IBoardComponent
    {

        Task<List<BoardSize>> GetBoardSizes();
        //Task<Board> SaveBoard(SaveBoardRequest board);
        Task<bool> DeleteBoard(int boardID);
        Task<List<Board>> GetBoardsByUserID(int userID);
        Task<Board> GetFullBoardByBoardID(int boardID);
        Task<bool> SaveBoardCards(int boardID, List<BoardCard> boardCards);
        Task<byte[]> GetBoardDocument(int boardID);
        Task<byte[]> GetBoardDocuments(List<int> boardIDs);
        Task<byte[]> DownloadBoards(Models.MVP.UserBoardsRequest request);
    }
}
