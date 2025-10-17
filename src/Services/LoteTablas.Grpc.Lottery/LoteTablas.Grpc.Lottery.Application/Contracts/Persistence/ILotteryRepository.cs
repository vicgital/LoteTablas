using LoteTablas.Grpc.Lottery.Domain.Entities;

namespace LoteTablas.Grpc.Lottery.Application.Contracts.Persistence
{
    public interface ILotteryRepository
    {
        Task<List<Domain.Entities.Lottery>> GetLotteriesByUserId(string userId);
        Task<List<Domain.Entities.Lottery>> GetLotteriesByLotteryType(string lotteryTypeId);
        Task<List<Domain.Entities.Lottery>> GetFreeLotteries();
        Task<Domain.Entities.Lottery?> GetLottery(string lotteryId);
        Task<List<LotteryType>> GetLotteryTypes();
        Task<(LotteryCards?, List<Card>)> GetLotteryAndCards(string lotteryId);
    }
}
