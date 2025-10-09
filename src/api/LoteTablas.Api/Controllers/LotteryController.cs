using LoteTablas.Api.Business.Components.Definition;
using LoteTablas.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoteTablas.Api.Controllers
{
    [Produces("application/json")]
    [Route("v1/lottery")]
    [ApiController]
    public class LotteryController(
        ILotteryComponent lotteryComponent,
        ILogger<LotteryController> logger,
        IConfiguration config) : BaseController<LotteryController>(logger, config)
    {

        private readonly ILotteryComponent _lotteryComponent = lotteryComponent;

        [HttpGet("", Name = "Get")]
        [ProducesResponseType(typeof(List<LotteryType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                await Task.Delay(0);
                return Ok(new
                {
                    message = "Hello World"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLotteryTypes()");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("types", Name = "GetLotteryTypes")]
        [ProducesResponseType(typeof(List<LotteryType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLotteryTypes()
        {
            try
            {
                var lotteryTypes = await _lotteryComponent.GetLotteryTypes();
                return Ok(lotteryTypes);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLotteryTypes()");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("free/list", Name = "GetFreeLotteries")]
        [ProducesResponseType(typeof(List<Lottery>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFreeLotteries()
        {
            try
            {
                var lotteryTypes = await _lotteryComponent.GetFreeLotteries();
                return Ok(lotteryTypes);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetFreeLotteries()");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("cards/{lotteryId}", Name = "GetLotteryCardsByLotteryID")]
        [ProducesResponseType(typeof(List<LotteryCard>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLotteryCardsByLotteryID(int lotteryId)
        {
            try
            {
                var cards = await _lotteryComponent.GetLotteryCardsByLotteryID(lotteryId);
                return Ok(cards);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetFreeLotteries()");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
