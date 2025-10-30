using LoteTablas.Api.Domain;

namespace LoteTablas.Api.Application.Contracts.Clients.Grpc
{
    public interface IBoardGrpcClient
    {
        Task<byte[]> GetBoardDocuments(BoardDocuments boardDocuments);
    }
}
