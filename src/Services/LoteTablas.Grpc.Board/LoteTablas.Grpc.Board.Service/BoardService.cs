namespace LoteTablas.Grpc.Board.Service
{
    public class BoardService(ILogger<BoardService> logger) : Definitions.Board.BoardBase
    {
        private readonly ILogger<BoardService> _logger = logger;
        
        
    }
}
