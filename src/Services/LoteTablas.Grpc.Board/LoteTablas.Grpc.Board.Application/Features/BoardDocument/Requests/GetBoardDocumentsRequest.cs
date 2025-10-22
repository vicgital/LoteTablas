using LoteTablas.Grpc.Board.Application.Features.BoardDocument.DTO;
using MediatR;

namespace LoteTablas.Grpc.Board.Application.Features.BoardDocument.Requests
{
    public record GetBoardDocumentsRequest(List<BoardDocumentRequestDto> BoardDocuments) : IRequest<byte[]>;
}
