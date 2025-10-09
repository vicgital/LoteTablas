using Dapper;
using LoteTablas.Core.Data.Interfaces;
using LoteTablas.Framework.Common.Database;
using System.Data;

namespace LoteTablas.Core.Data
{
    public class CardRepository(IDatabaseConnection databaseConnection) : ICardRepository
    {

        private readonly IDatabaseConnection _databaseConnection = databaseConnection;

        public async Task<Models.DAO.Card?> GetCard(int cardID)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var query = @"
                            SELECT [CardID], [Name], [Description], [ImageSmallPath], [ImageMediumPath], [ImageBigPath], [CreatorUserID]
                            FROM [Card] WITH(NOLOCK)
                            WHERE [Enabled] = 1 AND [CardID] = @CardID
                        ";

            var result = await conn.QuerySingleOrDefaultAsync<Models.DAO.Card>(query, new { CardID = cardID });
            return result;
        }

        public async Task<List<Models.DAO.Card>> GetCards(List<int> cardIds)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var query = @"
                            SELECT [CardID], [Name], [Description], [ImageSmallPath], [ImageMediumPath], [ImageBigPath], [CreatorUserID]
                            FROM [Card] WITH(NOLOCK)
                            WHERE [Enabled] = 1 AND [CardID] IN @CardIDs
                        ";

            var result = await conn.QueryAsync<Models.DAO.Card>(query, new { CardIds = cardIds.ToArray() });
            return result.ToList();
        }
    }
}
