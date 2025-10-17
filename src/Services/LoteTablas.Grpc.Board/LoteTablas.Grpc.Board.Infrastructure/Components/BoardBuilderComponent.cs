using LoteTablas.Grpc.Board.Application.Contracts.Components;
using LoteTablas.Grpc.Board.Application.Features.BoardBuilder.DTO;
using System.Text;

namespace LoteTablas.Grpc.Board.Infrastructure.Components
{
    public class BoardBuilderComponent : IBoardBuilderComponent
    {

        public async Task<string> GetBoardsDocumentHtml(List<DocumentBoardDto> boards)
        {
            var boardsHtml = new StringBuilder();
            foreach (var board in boards)
            {
                var boardHtml = await GetBoardDocumentHtml(board);
                boardsHtml.Append(boardHtml);
            }

            var html = GetInitialHtml().Replace("{{BoardHtml}}", boardsHtml.ToString());

            return html;
        }

        public async Task<string> GetBoardDocumentHtml(DocumentBoardDto board)
        {
            return BuildBoardHtml(4, 4, [.. board.BoardCards.OrderBy(e => e.Ordinal)]);
            
        }

        private static string BuildBoardHtml(int xSize, int ySize, List<DocumentBoardCardDto> boardCards)
        {
            var boardHtml = GetBoardHtml();

            var boardRowsHtml = new StringBuilder();

            var ordinal = 0;
            for (int x = 1; x <= xSize; x++)
            {
                var rowHtml = GetBoardRowHtml();

                var boardCellsHtml = new StringBuilder();

                for (int y = 1; y <= ySize; y++)
                {
                    ordinal++;
                    var card = boardCards.FirstOrDefault(e => e.Ordinal == ordinal);

                    var cardHtml = GetBoardCellHtml(card?.ImageUrl ?? string.Empty);

                    boardCellsHtml.Append(cardHtml);

                }

                rowHtml = rowHtml.Replace("{{BoardCellsHtml}}", boardCellsHtml.ToString());

                boardRowsHtml.Append(rowHtml);

            }

            boardHtml = boardHtml.Replace("{{BoardRowsHtml}}", boardRowsHtml.ToString());

            return boardHtml;

        }

        private static string GetInitialHtml()
        {
            return @"

                <!DOCTYPE html>
                <html>
                  <head>
                    <title>LoteTablas Board</title>
                    <style>
                      table {
                        border-collapse: collapse;
                      }

                      th,
                      td {
                        border: 1px solid black;
                        text-align: center;
                      }

                      td div {
                        width: 100px;
                        height: 160px;
                        background-size: cover;
                        background-repeat: no-repeat;
                      }
	  
	                  .board {
		                display: inline-block;
		                padding-left: 40px;
		                padding-top: 40px;
		
	                  }
	  
                    </style>
                  </head>
                  <body>
                        {{BoardHtml}}
                  </body>
                </html>


            ";
        }

        private static string GetBoardHtml()
        {
            return @"
                <div class=""board"">
		            <table>     
                        {{BoardRowsHtml}}       
                    </table>
	            </div>
            ";
        }

        private static string GetBoardRowHtml()
        {
            return @"
                <tr>
                    {{BoardCellsHtml}}
                </tr>
            ";
        }

        private static string GetBoardCellHtml(string cellBackgroundUrl)
        {
            return @$"
                <td>
                    <div style=""background-image: url('{cellBackgroundUrl}');""></div>
                </td>
            ";
        }

        
    }
}
