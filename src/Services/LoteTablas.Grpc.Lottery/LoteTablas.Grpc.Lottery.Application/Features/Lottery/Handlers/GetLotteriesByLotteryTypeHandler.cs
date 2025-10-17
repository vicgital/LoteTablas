using LoteTablas.Application.Contracts.Cache;
using LoteTablas.Grpc.Lottery.Application.Contracts.Persistence;
using LoteTablas.Grpc.Lottery.Application.Features.Lottery.DTO;
using LoteTablas.Grpc.Lottery.Application.Features.Lottery.Queries;
using MediatR;

namespace LoteTablas.Grpc.Lottery.Application.Features.Lottery.Handlers;

public class GetLotteriesByLotteryTypeHandler(
    ILotteryRepository repository,
    IMemoryCacheManager memoryCacheManager) : IRequestHandler<GetLotteriesByLotteryTypeQuery, List<LotteryDto>>
{
    private readonly ILotteryRepository _repository = repository;
    private readonly IMemoryCacheManager _memoryCache = memoryCacheManager;

    public async Task<List<LotteryDto>> Handle(GetLotteriesByLotteryTypeQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"Lotteries_LotteryType_{request.LotteryTypeId}";
        var result = _memoryCache.Get<List<LotteryDto>>(cacheKey);
        if (result == null)
        {
            var dbLotteries = await _repository.GetLotteriesByLotteryType(request.LotteryTypeId);
            result = dbLotteries == null ? [] : [.. dbLotteries.Select(e => LotteryDto.FromEntity(e))];
            if (result.Count > 0)
            {
                _memoryCache.Add<List<LotteryDto>>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_memoryCache.GetCacheDuration()));
            }
        }
        return result;
    }
}
