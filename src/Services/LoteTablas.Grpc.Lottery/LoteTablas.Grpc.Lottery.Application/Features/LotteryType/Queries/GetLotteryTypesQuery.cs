using LoteTablas.Grpc.Lottery.Application.Features.LotteryType.DTO;
using MediatR;

namespace LoteTablas.Grpc.Lottery.Application.Features.LotteryType.Queries
{
    public record GetLotteryTypesQuery() : IRequest<List<LotteryTypeDto>>;

}
