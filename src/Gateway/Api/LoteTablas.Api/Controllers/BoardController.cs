using LoteTablas.Api.Application.Features.Board.Requests;
using LoteTablas.Api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoteTablas.Api.Controllers
{
    [Produces("application/json")]
    [Route("v1/board")]
    [ApiController]
    public class BoardController(
        IMediator mediator,
        ILogger<BoardController> logger,
        IConfiguration config) : BaseController<BoardController>(logger, config)
    {

        private readonly IMediator _mediator = mediator;


        [HttpPost("document/download", Name = "DownloadBoards")]
        [Produces("application/pdf")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DownloadBoards([FromBody] BoardDocuments request)
        {
            if (ValidateUserBoardsRequest(request, out string message))
                return BadRequest(message);

            try
            {

                byte[] document = await _mediator.Send(new GetBoardDocumentsRequest(request));
                return File(document, "application/pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DownloadBoards()");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private static bool ValidateUserBoardsRequest(BoardDocuments request, out string message)
        {
            message = string.Empty;

            if (request is null)
                message = "Request is null";

            if (request?.Boards != null || request?.Boards.Count == 0)
                message = "Empty Boards";


            return string.IsNullOrWhiteSpace(message);

        }
    }
}
