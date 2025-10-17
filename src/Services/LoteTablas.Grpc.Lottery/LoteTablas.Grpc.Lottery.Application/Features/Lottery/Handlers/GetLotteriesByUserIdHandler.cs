using LoteTablas.Application.Contracts.Cache;
using LoteTablas.Grpc.Lottery.Application.Contracts.Persistence;
using LoteTablas.Grpc.Lottery.Application.Features.Lottery.DTO;
using LoteTablas.Grpc.Lottery.Application.Features.Lottery.Queries;
using MediatR;

namespace LoteTablas.Grpc.Lottery.Application.Features.Lottery.Handlers;

public class GetLotteriesByUserIdHandler(
    ILotteryRepository repository,
    IMemoryCacheManager memoryCacheManager) : IRequestHandler<GetLotteriesByUserIdQuery, List<LotteryDto>>
{
    private readonly ILotteryRepository _repository = repository;
    private readonly IMemoryCacheManager _memoryCache = memoryCacheManager;

    public async Task<List<LotteryDto>> Handle(GetLotteriesByUserIdQuery request, CancellationToken cancellationToken)
    {

        var cacheKey = $"Lotteries_User_{request.UserId}";
        var result = _memoryCache.Get<List<LotteryDto>>(cacheKey);
        if (result == null)
        {
            var dbLotteries = await _repository.GetLotteriesByUserId(request.UserId);
            result = dbLotteries == null ? [] : [.. dbLotteries.Select(e => LotteryDto.FromEntity(e))];
            if (result.Count > 0)
            {
                _memoryCache.Add<List<LotteryDto>>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_memoryCache.GetCacheDuration()));
            }
        }

        return result;


    }
}
