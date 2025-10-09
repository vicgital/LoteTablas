using LoteTablas.Core.Data.Interfaces;
using LoteTablas.Core.Models.DAO;

namespace LoteTablas.Core.Data.Mock;

public class LotteryRepository : ILotteryRepository
{
    public Task<List<Lottery>> GetFreeLotteries()
    {
        throw new NotImplementedException();
    }

    public Task<List<Lottery>> GetLotteriesByLotteryType(int lotteryTypeID)
    {
        throw new NotImplementedException();
    }

    public Task<List<Lottery>> GetLotteriesByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Lottery?> GetLottery(int lotteryID)
    {
        throw new NotImplementedException();
    }

    public async Task<List<LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID)
    {
        await Task.Delay(1000);
        return Data.LotteryCards;
    }

    public Task<List<LotteryType>> GetLotteryTypes()
    {
        throw new NotImplementedException();
    }
}
