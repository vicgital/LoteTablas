using LoteTablas.Core.Service.Definition;

namespace LoteTablas.Api.Data.Repositories.Definition
{
    public interface IBoardRepository
    {

        Task<List<BoardSizeModel>> GetBoardSizes();
        Task<BoardModel> SaveBoard(SaveBoardRequest board);
        Task<bool> DeleteBoard(int boardID);
        Task<List<BoardModel>> GetBoardsByUserID(int userID);
        Task<BoardModel> GetFullBoardByBoardID(int boardID);
        Task<bool> SaveBoardCards(int boardID, List<BoardCardModel> boardCards);
        Task<byte[]> GetBoardDocument(int boardID);
        Task<byte[]> GetBoardDocuments(List<int> boardIDs);
        Task<byte[]> GetUserBoardsDocument(UserBoardsLiteDocumentRequest request);

    }
}
