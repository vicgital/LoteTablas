using LoteTablas.Api.Data.Repositories.Definition;
using LoteTablas.Core.Service.Definition;
using static LoteTablas.Core.Service.Definition.Core;

namespace LoteTablas.Api.Data.Repositories.Implementation
{
    public class LotteryRepository(CoreClient coreClient) : ILotteryRepository
    {

        protected readonly CoreClient _coreClient = coreClient;

        public async Task<List<LotteryModel>> GetFreeLotteries()
        {
            var reply = await _coreClient.GetFreeLotteriesAsync(new EmptyRequest());
            return [.. reply.Lotteries];
        }

        public async Task<List<LotteryModel>> GetLotteriesByLotteryType(int lotteryTypeID)
        {
            var reply = await _coreClient.GetLotteriesByLotteryTypeAsync(new LotteryTypeIDRequest { LotteryTypeID = lotteryTypeID });
            return [.. reply.Lotteries];
        }

        public async Task<List<LotteryModel>> GetLotteriesByUserId(int userId)
        {
            var reply = await _coreClient.GetLotteriesByUserIdAsync(new UserIdRequest { UserID = userId });
            return [.. reply.Lotteries];
        }

        public async Task<LotteryModel> GetLottery(int lotteryID)
        {
            return await _coreClient.GetLotteryAsync(new LotteryIDRequest { LotteryID = lotteryID });
        }

        public async Task<List<LotteryCardModel>> GetLotteryCardsByLotteryID(int lotteryID)
        {
            var reply = await _coreClient.GetLotteryCardsByLotteryIDAsync(new LotteryIDRequest { LotteryID = lotteryID });
            return [.. reply.LotteryCards];
        }

        public async Task<List<LotteryTypeModel>> GetLotteryTypes()
        {
            var reply = await _coreClient.GetLotteryTypesAsync(new EmptyRequest());
            return [.. reply.LotteryTypes];
        }
    }
}
