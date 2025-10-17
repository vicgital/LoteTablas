namespace LoteTablas.Grpc.Board.Application.Contracts.Clients.Grpc
{
    public interface ICardGrpcClient
    {
        Task<List<Domain.Entities.Card>> GetCards(List<string> cardIds);
    }
}
