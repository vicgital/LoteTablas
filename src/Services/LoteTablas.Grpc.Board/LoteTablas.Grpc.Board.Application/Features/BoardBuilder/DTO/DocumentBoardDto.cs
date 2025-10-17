namespace LoteTablas.Grpc.Board.Application.Features.BoardBuilder.DTO
{
    public class DocumentBoardDto
    {
        public List<DocumentBoardCardDto> BoardCards { get; set; } = new();
    }
}
