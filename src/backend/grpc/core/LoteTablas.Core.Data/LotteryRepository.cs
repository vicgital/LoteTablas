using Dapper;
using LoteTablas.Core.Data.Interfaces;
using LoteTablas.Core.Models.DAO;
using LoteTablas.Framework.Common.Database;
using System.Data;

namespace LoteTablas.Core.Data
{
    public class LotteryRepository(IDatabaseConnection databaseConnection) : ILotteryRepository
    {

        private readonly IDatabaseConnection _databaseConnection = databaseConnection;

        public async Task<List<Lottery>> GetFreeLotteries()
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var sql = @"
                        SELECT 
                         L.[LotteryID]
                        ,L.[Name]
                        ,L.[Description]
                        ,L.[LotteryTypeID]
                        ,LT.[Name] as [LotteryType]
                        ,L.[OwnerUserID]
                        FROM [Lottery] AS L WITH (NOLOCK)
                        INNER JOIN [LotteryType] AS LT WITH (NOLOCK) ON L.[LotteryTypeID] = LT.[LotteryTypeID]
                        WHERE L.[Enabled] = 1 AND LT.[Enabled] = 1
                        AND L.[OwnerUserID] IS NULL
                        ";

            var result = await conn.QueryAsync<Lottery>(sql);
            return result.ToList();

        }

        public async Task<List<Lottery>> GetLotteriesByLotteryType(int lotteryTypeID)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var sql = @"
                        SELECT 
                         L.[LotteryID]
                        ,L.[Name]
                        ,L.[Description]
                        ,L.[LotteryTypeID]
                        ,LT.[Name] as [LotteryType]
                        ,L.[OwnerUserID]
                        FROM [Lottery] AS L WITH (NOLOCK)
                        INNER JOIN [LotteryType] AS LT WITH (NOLOCK) ON L.[LotteryTypeID] = LT.[LotteryTypeID]
                        WHERE L.[Enabled] = 1 AND LT.[Enabled] = 1
                        AND L.[LotteryTypeID] = @lotteryTypeID
                        AND L.[OwnerUserID] IS NULL
                        ";

            var result = await conn.QueryAsync<Lottery>(sql, new { lotteryTypeID });
            return result.ToList();
        }

        public async Task<List<Lottery>> GetLotteriesByUserId(int userId)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var sql = @"
                        SELECT 
                         L.[LotteryID]
                        ,L.[Name]
                        ,L.[Description]
                        ,L.[LotteryTypeID]
                        ,LT.[Name] as [LotteryType]
                        ,L.[OwnerUserID]
                        FROM [Lottery] AS L WITH (NOLOCK)
                        INNER JOIN [LotteryType] AS LT WITH (NOLOCK) ON L.[LotteryTypeID] = LT.[LotteryTypeID]
                        WHERE L.[Enabled] = 1 AND LT.[Enabled] = 1
                        AND L.[OwnerUserID] = @userId
                        ";

            var result = await conn.QueryAsync<Lottery>(sql, new { userId });
            return result.ToList();
        }

        public async Task<Lottery?> GetLottery(int lotteryID)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var sql = @"
                        SELECT 
                         L.[LotteryID]
                        ,L.[Name]
                        ,L.[Description]
                        ,L.[LotteryTypeID]
                        ,LT.[Name] as [LotteryType]
                        ,L.[OwnerUserID]
                        FROM [Lottery] AS L WITH (NOLOCK)
                        INNER JOIN [LotteryType] AS LT WITH (NOLOCK) ON L.[LotteryTypeID] = LT.[LotteryTypeID]
                        WHERE L.[Enabled] = 1 AND LT.[Enabled] = 1
                        AND L.[LotteryID] = @lotteryID
                        ";

            var result = await conn.QueryFirstOrDefaultAsync<Lottery>(sql, new { lotteryID });
            return result;
        }

        public async Task<List<LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID)
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var sql = @"
                        SELECT
                         LC.[CardID]
                        ,C.[Name]
                        ,C.[Description]
                        ,C.[ImageSmallPath]
                        ,C.[ImageMediumPath]
                        ,C.[ImageBigPath]
                        ,LC.[Ordinal]
                        FROM [LotteryCard] AS LC WITH(NOLOCK)
                        INNER JOIN [Card] AS C WITH(NOLOCK) ON LC.[CardID] = C.[CardID]
                        WHERE [LotteryID] = @lotteryID
                        AND LC.[Enabled] = 1 AND C.[Enabled] = 1
                        ORDER BY LC.[Ordinal]
                        ";

            var result = await conn.QueryAsync<LotteryCard>(sql, new { lotteryID });
            return result.ToList();
        }

        public async Task<List<LotteryType>> GetLotteryTypes()
        {
            using IDbConnection conn = _databaseConnection.GetDbSqlConnection();

            var sql = @"
                        SELECT
                         [LotteryTypeID]
                        ,[Name]
                        ,[Description]
                        ,[Code]
                        FROM [LotteryType] WITH (NOLOCK)
                        ";

            var result = await conn.QueryAsync<LotteryType>(sql);
            return result.ToList();
        }



    }
}
