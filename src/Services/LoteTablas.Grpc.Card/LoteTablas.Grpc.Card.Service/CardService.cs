namespace LoteTablas.Grpc.Card.Service
{
    public class CardService(ILogger<CardService> logger) : Definitions.Card.CardBase
    {
        private readonly ILogger<CardService> _logger = logger;
        
    }
    
}
