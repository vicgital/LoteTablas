using AutoMapper;
using Grpc.Core;
using LoteTablas.Grpc.Card.Application.Features.Card.DTO;
using LoteTablas.Grpc.Card.Application.Features.Card.Queries;
using LoteTablas.Grpc.Card.Service.Helpers;
using LoteTablas.Grpc.Definitions;
using MediatR;
using System.Reflection;

namespace LoteTablas.Grpc.Card.Service
{
    public class CardService(
        ILogger<CardService> logger,
        IMediator mediator,
        IMapper mapper
        ) : Definitions.Card.CardBase
    {
        private readonly ILogger<CardService> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        
        public async override Task<CardModel> GetCard(GetCardRequest request, ServerCallContext context)
        {
            RequestValidators.ValidateGetCardRequest(request);

            try
            {
                var card = await _mediator.Send(new GetCardQuery(request.CardId));

                var reply = new CardModel();

                if (card == null)
                    context.Status = new Status(StatusCode.NotFound, "NOT FOUND");
                else
                    reply = _mapper.Map<CardDto, CardModel>(card);

                return reply;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCard({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetCard() - Message: {ex.Message}"));
            }
        }

        public async override Task<CardsReply> GetCards(GetCardsRequest request, ServerCallContext context)
        {
            RequestValidators.ValidateGetCardsRequest(request);

            try
            {
                var cards = await _mediator.Send(new GetCardsQuery([.. request.CardIds]));

                var reply = new CardsReply();

                if (cards == null)
                    context.Status = new Status(StatusCode.NotFound, "NOT FOUND");
                else
                    reply.Cards.AddRange(_mapper.Map<List<CardDto>, List<CardModel>>(cards));

                return reply;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCards({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetCards() - Message: {ex.Message}"));
            }
        }


    }

}
