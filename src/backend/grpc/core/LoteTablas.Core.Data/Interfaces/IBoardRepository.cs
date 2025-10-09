using LoteTablas.Core.Models.DAO;

namespace LoteTablas.Core.Data.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board?> GetBoard(int boardID);
        Task<List<Board>> GetBoardsByUserID(int userID);
        Task<List<BoardSize>> GetBoardSizes();
        Task<BoardSize?> GetBoardSize(int boardSizeID);
        Task<List<BoardCard>> GetBoardCardsByBoardID(int boardID);
        Task<BoardTemplate?> GetBoardTemplate(int boardSizeID);



        Task<Board?> SaveBoard(Board board);
        Task<bool> DeleteBoard(int boardID);
        Task<bool> SaveBoardCards(int boardID, List<BoardCard> boardCards);

    }
}
