using LoteTablas.Blazor.UI.Models;
using Microsoft.AspNetCore.Components;

namespace LoteTablas.Blazor.UI.Components.Steps
{
    public partial class DownloadBoardsStepComponent
    {
        [Parameter]
        public List<LotteryCard> LotteryCards { get; set; } = [];

        [Parameter]
        public List<Board> UserBoards { get; set; } = [];

        [Parameter]
        public EventCallback OnAddNewBoard { get; set; }

        [Parameter]
        public EventCallback<Board> OnDeleteBoard { get; set; }

        [Parameter]
        public EventCallback<Board> OnEditBoard { get; set; }

        [Parameter]
        public EventCallback OnDownloadBoards { get; set; }


        private bool CanDownloadBoards => UserBoards.Count > 0;

        private bool CanAddBoards => UserBoards.Count < 4;        
    }
}
