using LoteTablas.Grpc.Board.Application.Contracts.Clients.Grpc;

namespace LoteTablas.Grpc.Board.Infrastructure.Clients.Grpc
{
    public class DocumentGrpcClient(Definitions.Document.DocumentClient documentGrpcClient) : IDocumentGrpcClient
    {
        private readonly Definitions.Document.DocumentClient _documentGrpcClient = documentGrpcClient;

        public async Task<byte[]> ConvertHtmlToPDF(string html)
        {
            var request = new Definitions.ConvertHtmlToPDFRequest
            {
                BodyHtml = html
            };

            var streamRequest = _documentGrpcClient.ConvertHtmlToPDF(request, deadline: DateTime.UtcNow.AddSeconds(10));

            List<byte> fileBytes = [];

            while (await streamRequest.ResponseStream.MoveNext(CancellationToken.None))
            {
                fileBytes.AddRange(streamRequest.ResponseStream.Current.Data.ToByteArray());
            }

            return [.. fileBytes];

        }

    }
}
