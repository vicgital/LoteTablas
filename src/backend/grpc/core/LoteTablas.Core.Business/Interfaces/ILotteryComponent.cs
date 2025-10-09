namespace LoteTablas.Core.Business.Interfaces
{
    public interface ILotteryComponent
    {
        Task<List<Models.Lottery>> GetLotteriesByUserId(int userId);
        Task<List<Models.Lottery>> GetLotteriesByLotteryType(int lotteryTypeID);
        Task<List<Models.Lottery>> GetFreeLotteries();
        Task<Models.Lottery?> GetLottery(int lotteryID);
        Task<List<Models.LotteryType>> GetLotteryTypes();
        Task<List<Models.LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID);
    }
}
