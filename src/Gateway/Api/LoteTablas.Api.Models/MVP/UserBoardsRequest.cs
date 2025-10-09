using System.Text.Json;
using System.Text.Json.Serialization;

namespace LoteTablas.Api.Models.MVP
{
    public record UserBoardsRequest
    {
        public List<Board> Boards { get; set; } = [];
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
