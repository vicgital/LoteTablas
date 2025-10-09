using AutoMapper;
using LoteTablas.Core.Business.Interfaces;
using LoteTablas.Core.Data.Interfaces;
using LoteTablas.Framework.Common.Cache;
using LoteTablas.Framework.Common.Configuration;

namespace LoteTablas.Core.Business
{
    public class LotteryComponent(IAppConfiguration config, IMapper mapper, ILotteryRepository lotteryRepository, IInMemoryCache inMemoryCache) : ILotteryComponent
    {

        private readonly IMapper _mapper = mapper;
        private readonly ILotteryRepository _lotteryRepository = lotteryRepository;
        private readonly IInMemoryCache _inMemoryCache = inMemoryCache;
        private readonly IAppConfiguration _config = config;
        private readonly double _cacheDuration = double.Parse(config.GetValue("CACHE_DURATION", "10"));

        public async Task<List<Models.Lottery>> GetFreeLotteries()
        {
            var cacheKey = $"Free_Lotteries";
            var result = _inMemoryCache.Get<List<Models.Lottery>>(cacheKey);

            if (result == null)
            {
                result = _mapper.Map<List<Models.DAO.Lottery>, List<Models.Lottery>>(await _lotteryRepository.GetFreeLotteries());
                _inMemoryCache.Add<List<Models.Lottery>>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_cacheDuration));
            }

            return result;
        }



        public async Task<List<Models.Lottery>> GetLotteriesByLotteryType(int lotteryTypeID)
        {
            var cacheKey = $"Lotteries_LotteryType_{lotteryTypeID}";
            var result = _inMemoryCache.Get<List<Models.Lottery>>(cacheKey);

            if (result == null)
            {
                result = _mapper.Map<List<Models.DAO.Lottery>, List<Models.Lottery>>(await _lotteryRepository.GetLotteriesByLotteryType(lotteryTypeID));
                _inMemoryCache.Add<List<Models.Lottery>>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_cacheDuration));
            }

            return result;
        }


        public async Task<List<Models.Lottery>> GetLotteriesByUserId(int userId)
        {
            var cacheKey = $"Lotteries_User_{userId}";
            var result = _inMemoryCache.Get<List<Models.Lottery>>(cacheKey);

            if (result == null)
            {
                result = _mapper.Map<List<Models.DAO.Lottery>, List<Models.Lottery>>(await _lotteryRepository.GetLotteriesByUserId(userId));
                _inMemoryCache.Add<List<Models.Lottery>>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_cacheDuration));
            }

            return result;

        }


        public async Task<Models.Lottery?> GetLottery(int lotteryID)
        {
            var cacheKey = $"Lottery_{lotteryID}";
            var result = _inMemoryCache.Get<Models.Lottery>(cacheKey);

            if (result == null)
            {
                var daoResult = await _lotteryRepository.GetLottery(lotteryID);
                if (daoResult == null)
                    return null;

                result = _mapper.Map<Models.DAO.Lottery, Models.Lottery>(daoResult);
                _inMemoryCache.Add<Models.Lottery>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_cacheDuration));
            }

            return result;
        }

        public async Task<List<Models.LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID)
        {
            var cacheKey = $"Lottery_Cards_LotteryID_{lotteryID}";
            var result = _inMemoryCache.Get<List<Models.LotteryCard>>(cacheKey);

            if (result == null)
            {
                result = _mapper.Map<List<Models.DAO.LotteryCard>, List<Models.LotteryCard>>(await _lotteryRepository.GetLotteryCardsByLotteryID(lotteryID));
                var baseUrl = _config.GetValue("STORAGE_ACCOUNT_BASE_URL");
                result.ForEach(x =>
                {
                    x.ImageBigPath = $"{baseUrl}{x.ImageBigPath}";
                    x.ImageMediumPath = $"{baseUrl}{x.ImageMediumPath}";
                    x.ImageSmallPath = $"{baseUrl}{x.ImageSmallPath}";
                });
                _inMemoryCache.Add<List<Models.LotteryCard>>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_cacheDuration));
            }

            return result;
        }

        public async Task<List<Models.LotteryType>> GetLotteryTypes()
        {
            var cacheKey = $"LotteryTypes";
            var result = _inMemoryCache.Get<List<Models.LotteryType>>(cacheKey);

            if (result == null)
            {
                result = _mapper.Map<List<Models.DAO.LotteryType>, List<Models.LotteryType>>(await _lotteryRepository.GetLotteryTypes());
                _inMemoryCache.Add<List<Models.LotteryType>>(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_cacheDuration));
            }

            return result;
        }

    }
}
