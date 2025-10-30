using LoteTablas.Api.Application.Contracts.Clients.Grpc;
using LoteTablas.Api.Application.Features.Lottery.Requests;
using LoteTablas.Api.Domain;
using MediatR;

namespace LoteTablas.Api.Application.Features.Lottery.Handlers
{
    public class GetLotteryCardsByLotteryHandler(ILotteryGrpcClient lotteryGrpcClient) : IRequestHandler<GetLotteryCardsByLotteryRequest, List<LotteryCard>>
    {
        private readonly ILotteryGrpcClient _lotteryGrpcClient = lotteryGrpcClient;

        public async Task<List<LotteryCard>> Handle(GetLotteryCardsByLotteryRequest request, CancellationToken cancellationToken)
        {
            return await _lotteryGrpcClient.GetLotteryCardsByLotteryId(request.LotteryId);

        }
    }
}
