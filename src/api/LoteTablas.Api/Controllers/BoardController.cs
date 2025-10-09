using LoteTablas.Api.Business.Components.Definition;
using LoteTablas.Api.Models.MVP;
using Microsoft.AspNetCore.Mvc;

namespace LoteTablas.Api.Controllers
{
    [Produces("application/json")]
    [Route("v1/board")]
    [ApiController]
    public class BoardController(
        IBoardComponent boardComponent,
        ILogger<BoardController> logger,
        IConfiguration config) : BaseController<BoardController>(logger, config)
    {

        private readonly IBoardComponent _boardComponent = boardComponent;

        [HttpGet("sizes", Name = "GetBoardSizes")]
        [ProducesResponseType(typeof(List<Models.BoardSize>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBoardSizes()
        {
            try
            {
                var boardSizes = await _boardComponent.GetBoardSizes();
                return Ok(boardSizes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBoardSizes()");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpGet("document/{boardId}", Name = "GetBoardDocument")]
        [Produces("application/pdf")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBoardDocument(int boardId)
        {
            if (boardId <= 0)
                return BadRequest("boardId must be specified");

            try
            {
                var document = await _boardComponent.GetBoardDocument(boardId);
                return File(document, "application/pdf");
            }
            catch (KeyNotFoundException kex)
            {
                _logger.LogWarning(kex, "GetBoardSizes()");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBoardSizes()");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost("document/download", Name = "DownloadBoards")]
        [Produces("application/pdf")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DownloadBoards([FromBody] UserBoardsRequest request)
        {
            if (ValidateUserBoardsRequest(request, out string message))
                return BadRequest(message);

            try
            {
                byte[] document = await _boardComponent.DownloadBoards(request);
                return File(document, "application/pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetBoardSizes()");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private static bool ValidateUserBoardsRequest(UserBoardsRequest request, out string message)
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
