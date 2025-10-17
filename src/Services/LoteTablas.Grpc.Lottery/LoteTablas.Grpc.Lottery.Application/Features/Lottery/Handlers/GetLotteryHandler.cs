using LoteTablas.Application.Contracts.Cache;
using LoteTablas.Grpc.Lottery.Application.Contracts.Persistence;
using LoteTablas.Grpc.Lottery.Application.Features.Lottery.DTO;
using LoteTablas.Grpc.Lottery.Application.Features.Lottery.Queries;
using MediatR;

namespace LoteTablas.Grpc.Lottery.Application.Features.Lottery.Handlers;

public class GetLotteryHandler(
    ILotteryRepository repository,
    IMemoryCacheManager memoryCacheManager
    ) : IRequestHandler<GetLotteryQuery, LotteryDto?>
{
    private readonly ILotteryRepository _repository = repository;
    private readonly IMemoryCacheManager _memoryCache = memoryCacheManager;

    public async Task<LotteryDto?> Handle(GetLotteryQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"Lottery_{request.LotteryId}";
        var result = _memoryCache.Get<LotteryDto>(cacheKey);
        if (result == null)
        {
            var dbLottery = await _repository.GetLottery(request.LotteryId);
            if (dbLottery == null) return null;
            result = LotteryDto.FromEntity(dbLottery);
            _memoryCache.Add<LotteryDto>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_memoryCache.GetCacheDuration()));
        }
        return result;
    }
}
