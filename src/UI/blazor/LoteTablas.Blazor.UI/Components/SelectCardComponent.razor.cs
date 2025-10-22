using LoteTablas.Blazor.UI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace LoteTablas.Blazor.UI.Components
{
    public partial class SelectCardComponent
    {

        [Parameter]
        public List<LotteryCard> LotteryCards { get; set; } = [];

        [Parameter]
        public EventCallback<LotteryCard> OnCardSelected { get; set; }


    }
}
