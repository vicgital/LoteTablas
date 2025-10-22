using BlazorBootstrap;
using LoteTablas.Blazor.UI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace LoteTablas.Blazor.UI.Components
{
    public partial class BoardBuilderComponent
    {
        [Parameter]
        public List<LotteryCard> LotteryCards { get; set; } = [];

        [Parameter]
        public Board CurrentBoard { get; set; } = new();

        [Parameter]
        public EventCallback<BoardCard> OnCardSelected { get; set; }

        [Parameter]
        public Modal SelectCardModal { get; set; } = default!;

        private int OrdinalSelected { get; set; } = 0;

        private async Task ShowSelectCardModal(int ordinal)
        {
            OrdinalSelected = ordinal;
            var availableCards = LotteryCards.Where(x => !CurrentBoard.BoardCards.Select(e => e.CardID).Contains(x.CardID)).ToList();
            var parameters = new Dictionary<string, object>
            {
                { "LotteryCards", availableCards },
                { "OnCardSelected", EventCallback.Factory.Create<LotteryCard>(this, SelectCard) }
            };

            await SelectCardModal.ShowAsync<SelectCardComponent>("Escoge una Carta", parameters: parameters);
        }

        private async Task SelectCard(LotteryCard card)
        {
            await OnCardSelected.InvokeAsync(new BoardCard { CardID = card.CardID, Ordinal = OrdinalSelected });
            await SelectCardModal.HideAsync();
            StateHasChanged();
        }

    }
}
