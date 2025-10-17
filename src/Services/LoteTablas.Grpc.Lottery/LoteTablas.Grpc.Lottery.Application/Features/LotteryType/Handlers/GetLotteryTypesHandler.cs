using LoteTablas.Application.Contracts.Cache;
using LoteTablas.Grpc.Lottery.Application.Contracts.Persistence;
using LoteTablas.Grpc.Lottery.Application.Features.LotteryType.DTO;
using LoteTablas.Grpc.Lottery.Application.Features.LotteryType.Queries;
using MediatR;

namespace LoteTablas.Grpc.Lottery.Application.Features.LotteryType.Handlers
{
    public class GetLotteryTypesHandler(
        ILotteryRepository repository,
        IMemoryCacheManager memoryCacheManager) : IRequestHandler<GetLotteryTypesQuery, List<LotteryTypeDto>>
    {
        private readonly ILotteryRepository _repository = repository;
        private readonly IMemoryCacheManager _memoryCache = memoryCacheManager;
        public async Task<List<LotteryTypeDto>> Handle(GetLotteryTypesQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "LotteryTypes_All";
            var result = _memoryCache.Get<List<LotteryTypeDto>>(cacheKey);
            if (result == null)
            {
                var dbLotteryTypes = await _repository.GetLotteryTypes();
                result = dbLotteryTypes == null ? [] : [.. dbLotteryTypes.Select(e => LotteryTypeDto.FromEntity(e))];
                _memoryCache.Add<List<LotteryTypeDto>>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_memoryCache.GetCacheDuration()));
            }
            return result;

        }
    }
}
