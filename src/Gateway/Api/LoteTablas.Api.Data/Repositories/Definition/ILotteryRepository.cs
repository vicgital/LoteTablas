using LoteTablas.Core.Service.Definition;

namespace LoteTablas.Api.Data.Repositories.Definition
{
    public interface ILotteryRepository
    {
        Task<List<LotteryModel>> GetLotteriesByUserId(int userId);
        Task<List<LotteryModel>> GetLotteriesByLotteryType(int lotteryTypeID);
        Task<List<LotteryModel>> GetFreeLotteries();
        Task<LotteryModel> GetLottery(int lotteryID);
        Task<List<LotteryTypeModel>> GetLotteryTypes();
        Task<List<LotteryCardModel>> GetLotteryCardsByLotteryID(int lotteryID);
    }
}
