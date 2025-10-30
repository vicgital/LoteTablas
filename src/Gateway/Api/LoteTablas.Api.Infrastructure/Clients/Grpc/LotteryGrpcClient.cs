using LoteTablas.Api.Application.Contracts.Clients.Grpc;
using LoteTablas.Api.Domain;
using LoteTablas.Grpc.Definitions;
using static LoteTablas.Grpc.Definitions.Lottery;

namespace LoteTablas.Api.Infrastructure.Clients.Grpc
{
    public class LotteryGrpcClient(LotteryClient grpcClient) : ILotteryGrpcClient
    {
        private readonly LotteryClient _grpcClient = grpcClient;

        public async Task<List<LotteryCard>> GetLotteryCardsByLotteryId(string lotteryId) 
        {
            var reply = await _grpcClient.GetLotteryCardsByLotteryIdAsync(new LotteryIdRequest { LotteryId = lotteryId });
            return [.. reply.LotteryCards.Select(MapLotteryCard)];
        }

        private static LotteryCard MapLotteryCard(LotteryCardModel lotteryCard) 
        {
            return new LotteryCard
            {
                CardId = lotteryCard.CardId,
                Name = lotteryCard.Name,
                Description = lotteryCard.Description,
                ImagePath = lotteryCard.ImagePath,
                Ordinal = lotteryCard.Ordinal
            };
        }

    }
}
