using LoteTablas.Grpc.Lottery.Domain.Entities;

namespace LoteTablas.Grpc.Lottery.Application.Interfaces.Repository
{
    public interface ILotteryRepository
    {
        public interface ILotteryRepository
        {
            Task<List<Domain.Entities.Lottery>> GetLotteriesByUserId(int userId);
            Task<List<Domain.Entities.Lottery>> GetLotteriesByLotteryType(int lotteryTypeID);
            Task<List<Domain.Entities.Lottery>> GetFreeLotteries();
            Task<Domain.Entities.Lottery?> GetLottery(int lotteryID);
            Task<List<LotteryType>> GetLotteryTypes();
            Task<List<LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID);
        }
    }
}
