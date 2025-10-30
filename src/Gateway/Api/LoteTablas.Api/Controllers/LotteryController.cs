using LoteTablas.Api.Application.Features.Lottery.Requests;
using LoteTablas.Api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoteTablas.Api.Controllers
{
    [Produces("application/json")]
    [Route("v1/lottery")]
    [ApiController]
    public class LotteryController(
        IMediator mediator,
        ILogger<LotteryController> logger,
        IConfiguration config) : BaseController<LotteryController>(logger, config)
    {

        private readonly IMediator _mediator = mediator;


        [HttpGet("cards/{lotteryId}", Name = "GetLotteryCardsByLotteryID")]
        [ProducesResponseType(typeof(List<LotteryCard>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLotteryCardsByLotteryID(string lotteryId)
        {
            try
            {
                var cards = await _mediator.Send(new GetLotteryCardsByLotteryRequest(lotteryId));
                return Ok(cards);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetLotteryCardsByLotteryID()");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
