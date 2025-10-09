using Grpc.Core;
using LoteTablas.Lottery.Service.Definition;

namespace LoteTablas.Lottery.Service.Services
{
    public class LotteryService(
        ILogger<LotteryService> logger) : Definition.Lottery.LotteryBase
    {

        private readonly ILogger<LotteryService> _logger = logger;


        public async override Task<LotteriesReply> GetLotteriesByUserId(UserIdRequest request, ServerCallContext context)
        {

            if (request.UserID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "UserId is required"));

            try
            {
                LotteriesReply reply = new();
                List<Models.Lottery> result = await _lotteryComponent.GetLotteriesByUserId(request.UserID);

                if (result != null)
                    reply.Lotteries.AddRange(_mapper.Map<List<Models.Lottery>, List<LotteryModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLotteriesByUserId({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteriesByUserId() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteriesReply> GetFreeLotteries(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                LotteriesReply reply = new();
                List<Models.Lottery> result = await _lotteryComponent.GetFreeLotteries();

                if (result != null)
                    reply.Lotteries.AddRange(_mapper.Map<List<Models.Lottery>, List<LotteryModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetFreeLotteries({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetFreeLotteries() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteriesReply> GetLotteriesByLotteryType(LotteryTypeIDRequest request, ServerCallContext context)
        {
            if (request.LotteryTypeID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "LotteryTypeID is required"));

            try
            {
                LotteriesReply reply = new();
                List<Models.Lottery> result = await _lotteryComponent.GetLotteriesByLotteryType(request.LotteryTypeID);

                if (result != null)
                    reply.Lotteries.AddRange(_mapper.Map<List<Models.Lottery>, List<LotteryModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLotteriesByLotteryType({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteriesByLotteryType() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteryModel> GetLottery(LotteryIDRequest request, ServerCallContext context)
        {
            if (request.LotteryID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "LotteryID is required"));

            try
            {
                LotteryModel reply = new();
                var result = await _lotteryComponent.GetLottery(request.LotteryID);

                if (result != null)
                    reply = _mapper.Map<Models.Lottery, LotteryModel>(result);
                else
                    context.Status = new Status(StatusCode.NotFound, "Lottery not found");

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLottery({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLottery() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteryCardsReply> GetLotteryCardsByLotteryID(LotteryIDRequest request, ServerCallContext context)
        {
            if (request.LotteryID <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "LotteryID is required"));


            try
            {
                LotteryCardsReply reply = new();
                List<Models.LotteryCard> result = await _lotteryComponent.GetLotteryCardsByLotteryID(request.LotteryID);

                if (result != null)
                    reply.LotteryCards.AddRange(_mapper.Map<List<Models.LotteryCard>, List<LotteryCardModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLotteryCardsByLotteryID({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteryCardsByLotteryID() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteryTypesReply> GetLotteryTypes(EmptyRequest request, ServerCallContext context)
        {

            try
            {
                LotteryTypesReply reply = new();
                List<Models.LotteryType> result = await _lotteryComponent.GetLotteryTypes();

                if (result != null)
                    reply.LotteryTypes.AddRange(_mapper.Map<List<Models.LotteryType>, List<LotteryTypeModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetLotteriesByLotteryType({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteriesByLotteryType() - Message: {ex.Message}"));
            }
        }


    }
}
