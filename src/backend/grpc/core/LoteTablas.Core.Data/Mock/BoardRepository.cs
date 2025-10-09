using LoteTablas.Core.Data.Interfaces;
using LoteTablas.Core.Models.DAO;

namespace LoteTablas.Core.Data.Mock;
public class BoardRepository : IBoardRepository
{
    public Task<bool> DeleteBoard(int boardID)
    {
        throw new NotImplementedException();
    }

    public Task<Board?> GetBoard(int boardID)
    {
        throw new NotImplementedException();
    }

    public Task<List<BoardCard>> GetBoardCardsByBoardID(int boardID)
    {
        throw new NotImplementedException();
    }

    public Task<List<Board>> GetBoardsByUserID(int userID)
    {
        throw new NotImplementedException();
    }

    public Task<BoardSize?> GetBoardSize(int boardSizeID)
    {
        throw new NotImplementedException();
    }

    public Task<List<BoardSize>> GetBoardSizes()
    {
        throw new NotImplementedException();
    }

    public Task<BoardTemplate?> GetBoardTemplate(int boardSizeID)
    {
        throw new NotImplementedException();
    }

    public Task<Board?> SaveBoard(Board board)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveBoardCards(int boardID, List<BoardCard> boardCards)
    {
        throw new NotImplementedException();
    }
}