using LoteTablas.Api.Models;

namespace LoteTablas.Api.Business.Components.Definition
{
    public interface ILotteryComponent
    {

        Task<List<Lottery>> GetLotteriesByUserId(int userId);
        Task<List<Lottery>> GetLotteriesByLotteryType(int lotteryTypeID);
        Task<List<Lottery>> GetFreeLotteries();
        Task<Lottery?> GetLottery(int lotteryID);
        Task<List<LotteryType>> GetLotteryTypes();
        Task<List<LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID);
    }
}
