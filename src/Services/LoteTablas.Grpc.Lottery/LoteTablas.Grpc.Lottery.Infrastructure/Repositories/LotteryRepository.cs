using Dapper;
using LoteTablas.Grpc.Lottery.Application.Contracts.Persistence;
using LoteTablas.Grpc.Lottery.Domain.Entities;
using System.Data;

namespace LoteTablas.Grpc.Lottery.Infrastructure.Repositories
{
    public class LotteryRepository(IDbConnection dbConnection) : ILotteryRepository
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task<List<Domain.Entities.Lottery>> GetFreeLotteries()
        {
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

            var result = await _dbConnection.QueryAsync<Domain.Entities.Lottery>(sql);
            return [.. result];

        }

        public async Task<List<Domain.Entities.Lottery>> GetLotteriesByLotteryType(int lotteryTypeID)
        {
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

            var result = await _dbConnection.QueryAsync<Domain.Entities.Lottery>(sql, new { lotteryTypeID });
            return [.. result];

        }

        public async Task<List<Domain.Entities.Lottery>> GetLotteriesByUserId(int userId)
        {
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

            var result = await _dbConnection.QueryAsync<Domain.Entities.Lottery>(sql, new { userId });
            return [.. result];

        }

        public async Task<Domain.Entities.Lottery?> GetLottery(int lotteryID)
        {   

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

            var result = await _dbConnection.QueryFirstOrDefaultAsync<Domain.Entities.Lottery>(sql, new { lotteryID });
            return result;
        }

        public async Task<List<LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID)
        {
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

            var result = await _dbConnection.QueryAsync<LotteryCard>(sql, new { lotteryID });
            return [.. result];
        }

        public async Task<List<LotteryType>> GetLotteryTypes()
        {

            var sql = @"
                        SELECT
                         [LotteryTypeID]
                        ,[Name]
                        ,[Description]
                        ,[Code]
                        FROM [LotteryType] WITH (NOLOCK)
                        ";

            var result = await _dbConnection.QueryAsync<LotteryType>(sql);
            return [.. result];
        }
    }
}
