using LoteTablas.Blazor.UI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace LoteTablas.Blazor.UI.Components
{
    public partial class BoardComponent
    {
        [Parameter]
        public List<LotteryCard> LotteryCards { get; set; } = [];
        
        [Parameter]
        public Board Board { get; set; } = new();

        [Parameter]
        public EventCallback<Board> OnDeleteBoard { get; set; }

        [Parameter]
        public EventCallback<Board> OnEditBoard { get; set; }

    }
}
