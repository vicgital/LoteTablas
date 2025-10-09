using LoteTablas.Core.Models.DAO;

namespace LoteTablas.Core.Data.Interfaces
{
    public interface ILotteryRepository
    {
        Task<List<Lottery>> GetLotteriesByUserId(int userId);
        Task<List<Lottery>> GetLotteriesByLotteryType(int lotteryTypeID);
        Task<List<Lottery>> GetFreeLotteries();
        Task<Lottery?> GetLottery(int lotteryID);
        Task<List<LotteryType>> GetLotteryTypes();
        Task<List<LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID);
    }
}
