using Dapper;
using LoteTablas.Core.Data.Interfaces;
using LoteTablas.Core.Models.DAO;
using LoteTablas.Framework.Common.Database;
using System.Data;
using System.Text;

namespace LoteTablas.Core.Data
{
    public class BoardRepository(IDatabaseConnection databaseConnection) : IBoardRepository
    {

        private readonly IDatabaseConnection _databaseConnection = databaseConnection;

        public async Task<Board?> GetBoard(int boardID)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var query = @"
                            SELECT [BoardID], [Name], [BoardSizeID], [LotteryID], [FileUrl], [OwnerUserID]
                            FROM [Board] WITH(NOLOCK)
                            WHERE [Enabled] = 1 AND [BoardID] = @BoardID
                        ";

            var result = await conn.QuerySingleOrDefaultAsync<Board>(query, new { BoardID = boardID });
            return result;
        }
        public async Task<List<BoardSize>> GetBoardSizes()
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var query = @"
                            SELECT [BoardSizeID], [Code], [XSize], [YSize]
                            FROM [BoardSize] WITH(NOLOCK)
                            WHERE [Enabled] = 1
                        ";

            var result = await conn.QueryAsync<BoardSize>(query);
            return result.ToList();
        }
        public async Task<List<BoardCard>> GetBoardCardsByBoardID(int boardID)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var query = @"
                            SELECT [CardID], [Ordinal]
                            FROM [BoardCard] WITH(NOLOCK)
                            WHERE [Enabled] = 1 AND [BoardID] = @BoardID
                        ";

            var result = await conn.QueryAsync<BoardCard>(query, new { BoardID = boardID });
            return result.ToList();
        }
        public async Task<List<Board>> GetBoardsByUserID(int userID)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var query = @"
                            SELECT [BoardID], [Name], [BoardSizeID], [LotteryID], [FileUrl], [OwnerUserID]
                            FROM [Board] WITH(NOLOCK)
                            WHERE [Enabled] = 1 AND [OwnerUserID] = @UserID
                        ";

            var result = await conn.QueryAsync<Models.DAO.Board>(query, new { UserID = userID });
            return result.ToList();
        }
        public async Task<Board?> SaveBoard(Board board)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var query = @"
                            INSERT INTO [Board]([Name], [BoardSizeID], [LotteryID], [OwnerUserID])
                            OUTPUT INSERTED.*
                            VALUES (@Name, @BoardSizeID, @LotteryID, @OwnerUserID)
                        ";

            var result = await conn.QuerySingleOrDefaultAsync<Board>(query, new { Name = board.Name ?? string.Empty, board.BoardSizeID, board.LotteryID, board.OwnerUserID });

            return result;
        }
        public async Task<bool> DeleteBoard(int boardID)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var query = @"
                            UPDATE [Board] SET Enabled = 0, [DateModified] = GETDATE()
                            FROM [Board] WITH(NOLOCK)
                            WHERE [BoardID] = @BoardID
                        ";

            var result = await conn.ExecuteAsync(query, new { BoardID = boardID });
            return result > 0;
        }
        public async Task<BoardSize?> GetBoardSize(int boardSizeID)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var query = @"
                            SELECT [BoardSizeID], [Code], [XSize], [YSize]
                            FROM [BoardSize] WITH(NOLOCK)
                            WHERE [Enabled] = 1 AND [BoardSizeID] = @BoardSizeID
                        ";

            var result = await conn.QueryFirstOrDefaultAsync<BoardSize>(query, new { BoardSizeID = boardSizeID });
            return result;
        }
        public async Task<BoardTemplate?> GetBoardTemplate(int boardSizeID)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var query = @"
                            SELECT *
                            FROM [Board] WITH(NOLOCK)
                            WHERE [Enabled] = 1 AND [BoardSizeID] = @BoardSizeID
                        ";

            var result = await conn.QuerySingleOrDefaultAsync<Models.DAO.BoardTemplate>(query, new { BoardSizeID = boardSizeID });
            return result;
        }
        public async Task<bool> SaveBoardCards(int boardID, List<BoardCard> boardCards)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();
            StringBuilder queryBuilder = new();

            queryBuilder.AppendLine("BEGIN TRANSACTION");

            queryBuilder.AppendLine(@$"

                                            DELETE FROM [BoardCard]
                                            WHERE [BoardID] = {boardID}

                                        ");

            foreach (var boardCard in boardCards)
            {
                queryBuilder.AppendLine(@$"

                                            INSERT INTO [BoardCard]([BoardID], [CardID], [Ordinal])
                                            VALUES ({boardID}, {boardCard.CardID}, {boardCard.Ordinal})

                                        ");
            }

            queryBuilder.AppendLine("COMMIT TRANSACTION");

            var result = await conn.ExecuteAsync(queryBuilder.ToString());

            return result > 0;
        }
    }
}
