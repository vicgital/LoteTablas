using LoteTablas.Grpc.Lottery.Application.Features.Lottery.DTO;
using MediatR;

namespace LoteTablas.Grpc.Lottery.Application.Features.Lottery.Queries
{
    public record GetLotteriesByLotteryTypeQuery(string LotteryTypeId) : IRequest<List<LotteryDto>>;

}
