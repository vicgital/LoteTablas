using LoteTablas.Api.Domain;

namespace LoteTablas.Api.Application.Contracts.Clients.Grpc
{
    public interface ILotteryGrpcClient
    {
        Task<List<LotteryCard>> GetLotteryCardsByLotteryId(string lotteryId);
    }
}
