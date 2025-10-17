using MediatR;

namespace LoteTablas.Grpc.Lottery.Application.Features.LotteryCard.Queries
{
    public record GetLotteryCardsByLotteryIdQuery(string LotteryId) : IRequest<List<DTO.LotteryCardDto>>;
}
