using AutoMapper;
using Grpc.Core;
using LoteTablas.Api.Business.Components.Definition;
using LoteTablas.Api.Data.Repositories.Definition;
using LoteTablas.Api.Models;
using LoteTablas.Core.Service.Definition;
using Microsoft.Extensions.Logging;

namespace LoteTablas.Api.Business.Components.Implementation
{
    public class CardComponent(IMapper mapper, ILogger<CardComponent> logger, ICardRepository cardRepository) : ICardComponent
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<CardComponent> _logger = logger;
        private readonly ICardRepository _cardRepository = cardRepository;

        public async Task<Models.Card?> GetCard(int cardID)
        {
            try
            {
                return _mapper.Map<CardModel, Card>(await _cardRepository.GetCard(cardID));
            }
            catch (Exception ex)
            {
                if (ex is RpcException { Status.StatusCode: StatusCode.NotFound } || ex.InnerException is RpcException { Status.StatusCode: StatusCode.NotFound })
                    _logger.LogWarning("NOTFOUND GetCard({cardID})", cardID);
                else
                    throw;
            }

            return null;
        }

        public async Task<List<Models.Card>> GetCards(List<int> cardIds) =>
            _mapper.Map<List<CardModel>, List<Card>>(await _cardRepository.GetCards(cardIds));

    }
}
