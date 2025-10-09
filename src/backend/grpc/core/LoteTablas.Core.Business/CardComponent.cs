using AutoMapper;
using LoteTablas.Core.Business.Interfaces;
using LoteTablas.Core.Data.Interfaces;
using LoteTablas.Framework.Common.Cache;
using LoteTablas.Framework.Common.Configuration;

namespace LoteTablas.Core.Business
{
    public class CardComponent(IAppConfiguration config, IMapper mapper, ICardRepository cardRepository, IInMemoryCache inMemoryCache) : ICardComponent
    {
        private readonly ICardRepository _cardRepository = cardRepository;
        private readonly IInMemoryCache _inMemoryCache = inMemoryCache;
        private readonly IMapper _mapper = mapper;
        private readonly double _cacheDuration = double.Parse(config.GetValue("CACHE_DURATION", "10"));

        public async Task<Models.Card?> GetCard(int cardID)
        {

            var cacheKey = $"Card_{cardID}";
            var result = _inMemoryCache.Get<Models.Card>(cacheKey);

            if (result == null)
            {
                var cardDAO = await _cardRepository.GetCard(cardID);
                if (cardDAO is not null)
                {
                    result = _mapper.Map<Models.DAO.Card, Models.Card>(cardDAO);
                    _inMemoryCache.Add(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(_cacheDuration));
                }
            }

            return result;

        }

        public async Task<List<Models.Card>> GetCards(List<int> cardIds)
        {
            var cardsDAO = await _cardRepository.GetCards(cardIds);
            return _mapper.Map<List<Models.DAO.Card>, List<Models.Card>>(cardsDAO);
        }
    }


}
