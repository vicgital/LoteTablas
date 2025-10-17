namespace LoteTablas.Grpc.Board.Application.Contracts.Clients.Grpc
{
    public interface IDocumentGrpcClient
    {
        Task<byte[]> ConvertHtmlToPDF(string html);
    }
}
