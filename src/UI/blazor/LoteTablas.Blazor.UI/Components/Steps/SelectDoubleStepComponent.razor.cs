using BlazorBootstrap;
using LoteTablas.Blazor.UI.Models;
using Microsoft.AspNetCore.Components;

namespace LoteTablas.Blazor.UI.Components.Steps
{
    partial class SelectDoubleStepComponent
    {
        #region Parameters

        [Parameter]
        public List<LotteryCard> LotteryCards { get; set; } = [];

        [Parameter]
        public EventCallback<Board> OnSelectDoubleCardCompleted { get; set; }

        [Parameter]
        public Board CurrentBoard { get; set; } = new();

        [Parameter]
        public Modal SelectCardModal { get; set; } = default!;

        #endregion



        #region private properties

        private int HasDoubleCardValue { get; set; } = 0;
        private bool HasDoubleCard => HasDoubleCardValue > 0;
        private LotteryCard? CardSelected { get; set; } = null;

        private bool CanContinue => !HasDoubleCard ||
                                        (HasDoubleCard &&
                                        CardSelected != null &&
                                        DoubleCardPosition1 > 0 &&
                                        DoubleCardPosition2 > 0);

        private int DoubleCardPosition1 { get; set; } = 0;
        private int DoubleCardPosition2 { get; set; } = 0;

        #endregion

        protected override void OnInitialized()
        {

            if (CurrentBoard != null)
            {
                if (CurrentBoard.DoubleCardId > 0)
                {
                    HasDoubleCardValue = 1;
                    CardSelected = LotteryCards.FirstOrDefault(x => x.CardID == CurrentBoard.DoubleCardId);
                    DoubleCardPosition1 = CurrentBoard.DoubleCardPosition.Item1;
                    DoubleCardPosition2 = CurrentBoard.DoubleCardPosition.Item2;
                }
            }

            base.OnInitialized();
        }
        

        private async Task ShowSelectCardModal()
        {
            var parameters = new Dictionary<string, object>
            {
                { "LotteryCards", LotteryCards },
                { "OnCardSelected", EventCallback.Factory.Create<LotteryCard>(this, SelectDoubleCard) }
            };

            await SelectCardModal.ShowAsync<SelectCardComponent>("Escoge una Carta", parameters: parameters);
        }

        private async Task SelectDoubleCard(LotteryCard card)
        {
            CardSelected = card;
            await SelectCardModal.HideAsync();
        }

        private void SelectDoubleCardPosition(Tuple<int,int> doubleCardPosition) 
        {
            DoubleCardPosition1 = doubleCardPosition.Item1;
            DoubleCardPosition2 = doubleCardPosition.Item2;
        }

        private void Reset() 
        {
            HasDoubleCardValue = 0;
            CardSelected = null;
            DoubleCardPosition1 = 0;
            DoubleCardPosition2 = 0;
        }

        private async Task SelectDoubleCardComplete() 
        {
            if (HasDoubleCard && CardSelected != null)
            {
                CurrentBoard.DoubleCardId = CardSelected.CardID;
                CurrentBoard.DoubleCardPosition = Tuple.Create(DoubleCardPosition1, DoubleCardPosition2);
                CurrentBoard.BoardCards.Clear();
                CurrentBoard.BoardCards.Add(new BoardCard { CardID = CardSelected.CardID, Ordinal = DoubleCardPosition1 });
                CurrentBoard.BoardCards.Add(new BoardCard { CardID = CardSelected.CardID, Ordinal = DoubleCardPosition2 });
            }
            else
            {
                CurrentBoard = new(CurrentBoard.BoardId);
            }

            await OnSelectDoubleCardCompleted.InvokeAsync(CurrentBoard);
        }

    }
}
