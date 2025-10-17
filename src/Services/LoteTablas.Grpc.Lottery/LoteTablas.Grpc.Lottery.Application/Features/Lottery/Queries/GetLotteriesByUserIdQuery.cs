using LoteTablas.Grpc.Lottery.Application.Features.Lottery.DTO;
using MediatR;

namespace LoteTablas.Grpc.Lottery.Application.Features.Lottery.Queries
{
    public record GetLotteriesByUserIdQuery(string UserId) : IRequest<List<LotteryDto>>;
}
