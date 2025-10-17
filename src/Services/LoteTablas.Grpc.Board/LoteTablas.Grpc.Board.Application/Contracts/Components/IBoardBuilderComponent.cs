using LoteTablas.Grpc.Board.Application.Features.BoardBuilder.DTO;

namespace LoteTablas.Grpc.Board.Application.Contracts.Components
{
    public interface IBoardBuilderComponent
    {
        Task<string> GetBoardsDocumentHtml(List<DocumentBoardDto> boards);
        Task<string> GetBoardDocumentHtml(DocumentBoardDto boards);
    }
}
