using AutoMapper;
using Grpc.Core;
using LoteTablas.Grpc.Definitions;
using LoteTablas.Grpc.Definitions.Common;
using LoteTablas.Grpc.Lottery.Application.Features.Lottery.DTO;
using LoteTablas.Grpc.Lottery.Application.Features.Lottery.Queries;
using LoteTablas.Grpc.Lottery.Application.Features.LotteryCard.DTO;
using LoteTablas.Grpc.Lottery.Application.Features.LotteryCard.Queries;
using LoteTablas.Grpc.Lottery.Application.Features.LotteryType.DTO;
using LoteTablas.Grpc.Lottery.Application.Features.LotteryType.Queries;
using MediatR;

namespace LoteTablas.Grpc.Lottery.Service
{
    public class LotteryService(
        ILogger<LotteryService> logger,
        IMediator mediator,        
        IMapper mapper) : Definitions.Lottery.LotteryBase
    {
        private readonly ILogger<LotteryService> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public async override Task<LotteriesReply> GetLotteriesByUserId(UserIdRequest request, ServerCallContext context)
        {

            if (string.IsNullOrEmpty(request.UserId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "UserId is required"));

            try
            {
                LotteriesReply reply = new();
                var result = await _mediator.Send(new GetLotteriesByUserIdQuery(request.UserId));

                if (result != null)
                    reply.Lotteries.AddRange(_mapper.Map<List<LotteryDto>, List<LotteryModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLotteriesByUserId({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteriesByUserId() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteriesReply> GetFreeLotteries(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                LotteriesReply reply = new();
                var result = await _mediator.Send(new GetFreeLotteriesQuery());

                if (result != null)
                    reply.Lotteries.AddRange(_mapper.Map<List<LotteryDto>, List<LotteryModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetFreeLotteries({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetFreeLotteries() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteriesReply> GetLotteriesByLotteryType(LotteryTypeIdRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.LotteryTypeId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "LotteryTypeID is required"));

            try
            {
                LotteriesReply reply = new();
                var result = await _mediator.Send(new GetLotteriesByLotteryTypeQuery(request.LotteryTypeId));

                if (result != null)
                    reply.Lotteries.AddRange(_mapper.Map<List<LotteryDto>, List<LotteryModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLotteriesByLotteryType({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteriesByLotteryType() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteryModel> GetLottery(LotteryIdRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.LotteryId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "LotteryId is required"));

            try
            {
                LotteryModel reply = new();
                var result = await _mediator.Send(new GetLotteryQuery(request.LotteryId));

                if (result != null)
                    reply = _mapper.Map<LotteryDto, LotteryModel>(result);
                else
                    context.Status = new Status(StatusCode.NotFound, "Lottery not found");

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLottery({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLottery() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteryCardsReply> GetLotteryCardsByLotteryId(LotteryIdRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.LotteryId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "LotteryID is required"));


            try
            {
                LotteryCardsReply reply = new();
                var result = await _mediator.Send(new GetLotteryCardsByLotteryIdQuery(request.LotteryId));

                if (result != null)
                    reply.LotteryCards.AddRange(_mapper.Map<List<LotteryCardDto>, List<LotteryCardModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLotteryCardsByLotteryID({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteryCardsByLotteryID() - Message: {ex.Message}"));
            }
        }

        public async override Task<LotteryTypesReply> GetLotteryTypes(EmptyRequest request, ServerCallContext context)
        {

            try
            {
                LotteryTypesReply reply = new();
                var result = await _mediator.Send(new GetLotteryTypesQuery());

                if (result != null)
                    reply.LotteryTypes.AddRange(_mapper.Map<List<LotteryTypeDto>, List<LotteryTypeModel>>(result));

                return reply;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLotteriesByLotteryType({request})", request);
                throw new RpcException(new Status(StatusCode.Internal, $"Error GetLotteriesByLotteryType() - Message: {ex.Message}"));
            }
        }


    }
}
