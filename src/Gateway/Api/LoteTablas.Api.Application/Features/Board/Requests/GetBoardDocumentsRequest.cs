using LoteTablas.Api.Domain;
using MediatR;

namespace LoteTablas.Api.Application.Features.Board.Requests
{
    public record GetBoardDocumentsRequest(BoardDocuments Request) : IRequest<byte[]>;

}
