using MediatR;

namespace LoteTablas.Grpc.Card.Application.Features.Card.Queries
{
    public record GetCardQuery(string CardId) : IRequest<DTO.CardDto?>;

}
