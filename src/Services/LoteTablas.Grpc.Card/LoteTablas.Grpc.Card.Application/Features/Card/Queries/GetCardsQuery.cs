using MediatR;

namespace LoteTablas.Grpc.Card.Application.Features.Card.Queries
{
    public record GetCardsQuery(List<string> CardIds) : IRequest<List<DTO.CardDto>>;

}
