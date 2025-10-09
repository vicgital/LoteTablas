using AutoMapper;
using Grpc.Core;
using LoteTablas.Api.Business.Components.Definition;
using LoteTablas.Api.Data.Repositories.Definition;
using LoteTablas.Api.Models;
using LoteTablas.Core.Service.Definition;
using Microsoft.Extensions.Logging;

namespace LoteTablas.Api.Business.Components.Implementation
{
    public class LotteryComponent(
        IMapper mapper,
        ILogger<LotteryComponent> logger,
        ILotteryRepository lotteryRepository
        ) : ILotteryComponent
    {
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<LotteryComponent> _logger = logger;
        private readonly ILotteryRepository _lotteryRepository = lotteryRepository;

        public async Task<List<Lottery>> GetFreeLotteries() =>
            _mapper.Map<List<LotteryModel>, List<Lottery>>(await _lotteryRepository.GetFreeLotteries());

        public async Task<List<Lottery>> GetLotteriesByLotteryType(int lotteryTypeID) =>
            _mapper.Map<List<LotteryModel>, List<Lottery>>(await _lotteryRepository.GetLotteriesByLotteryType(lotteryTypeID));

        public async Task<List<Lottery>> GetLotteriesByUserId(int userId)=>
            _mapper.Map<List<LotteryModel>, List<Lottery>>(await _lotteryRepository.GetLotteriesByUserId(userId));

        public async Task<Lottery?> GetLottery(int lotteryID)
        {
            try
            {
                return _mapper.Map<LotteryModel, Lottery>(await _lotteryRepository.GetLottery(lotteryID));
            }
            catch (Exception ex)
            {
                if (ex is RpcException { Status.StatusCode: StatusCode.NotFound } || ex.InnerException is RpcException { Status.StatusCode: StatusCode.NotFound })
                    _logger.LogWarning("NOTFOUND GetLottery({lotteryID})", lotteryID);
                else
                    throw;
            }

            return null;
        }


        public async Task<List<LotteryCard>> GetLotteryCardsByLotteryID(int lotteryID) =>
            _mapper.Map<List<LotteryCardModel>, List<LotteryCard>>(await _lotteryRepository.GetLotteryCardsByLotteryID(lotteryID));


        public async Task<List<LotteryType>> GetLotteryTypes() =>
            _mapper.Map<List<LotteryTypeModel>, List<LotteryType>>(await _lotteryRepository.GetLotteryTypes());
    }
}
