using Grpc.Core;
using LoteTablas.Grpc.Definitions;

namespace LoteTablas.Grpc.Card.Service.Helpers
{
    public static class RequestValidators
    {
        public static void ValidateGetCardsRequest(GetCardsRequest request)
        {
            if (request == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Invalid request"));

            if (request.CardIds.Count == 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"CardIDs is required"));


        }

        public static void ValidateGetCardRequest(GetCardRequest request)
        {
            if (request == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Invalid request"));

            if (string.IsNullOrEmpty(request.CardId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Invalid CardId"));
        }
    }
}
