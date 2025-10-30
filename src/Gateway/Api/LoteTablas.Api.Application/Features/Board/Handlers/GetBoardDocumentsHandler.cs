using LoteTablas.Api.Application.Contracts.Clients.Grpc;
using LoteTablas.Api.Application.Features.Board.Requests;
using MediatR;

namespace LoteTablas.Api.Application.Features.Board.Handlers
{
    public class GetBoardDocumentsHandler(IBoardGrpcClient boardGrpcClient) : IRequestHandler<GetBoardDocumentsRequest, byte[]>
    {
        private readonly IBoardGrpcClient _boardGrpcClient = boardGrpcClient;

        public async Task<byte[]> Handle(GetBoardDocumentsRequest request, CancellationToken cancellationToken)
        {
            return await _boardGrpcClient.GetBoardDocuments(request.Request);
        }
    }
}
