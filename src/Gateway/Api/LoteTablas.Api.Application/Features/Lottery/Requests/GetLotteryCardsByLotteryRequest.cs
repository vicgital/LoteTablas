using LoteTablas.Api.Domain;
using MediatR;

namespace LoteTablas.Api.Application.Features.Lottery.Requests
{
    public record GetLotteryCardsByLotteryRequest(string LotteryId) : IRequest<List<LotteryCard>>;

}
