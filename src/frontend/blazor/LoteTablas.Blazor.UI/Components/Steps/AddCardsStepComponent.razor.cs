using BlazorBootstrap;
using LoteTablas.Blazor.UI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LoteTablas.Blazor.UI.Components.Steps
{
    public partial class AddCardsStepComponent
    {
        [Parameter]
        public List<LotteryCard> LotteryCards { get; set; } = [];

        [Parameter]
        public Board CurrentBoard { get; set; } = new();

        private bool IsBoardCompleted => CurrentBoard.BoardCards.Count == 16;

        [Parameter]
        public EventCallback OnBack { get; set; }

        [Parameter]
        public EventCallback<Board> OnAddCardsCompleted { get; set; }

        [Parameter]
        public Modal SelectCardModal { get; set; } = default!;

        private void OnCardSelected(BoardCard card)
        {
            if (CurrentBoard.BoardCards.Any(e => e.Ordinal == card.Ordinal))
            {
                // replace card
                CurrentBoard.BoardCards.First(e => e.Ordinal == card.Ordinal).CardID = card.CardID;
            }
            else
                CurrentBoard.BoardCards.Add(card);
            
            StateHasChanged();
        }

        private void AddRandomCards()
        {
            // Add random cards to the board
            List<LotteryCard> availableLotteryCards =  [];
            int cardsToAdd;

            if (CurrentBoard.BoardCards.Count == 16)
            {
                CleanBoard();
                availableLotteryCards = LotteryCards.Where(x => x.CardID != CurrentBoard.DoubleCardId).ToList();
                cardsToAdd = CurrentBoard.DoubleCardId == 0 ? 16 : 14;
            }
            else
            {
                var currentBoardCardIds = CurrentBoard.BoardCards.Select(x => x.CardID).ToList();
                cardsToAdd = 16 - currentBoardCardIds.Count;
                availableLotteryCards = LotteryCards.Where(x => !currentBoardCardIds.Contains(x.CardID)).ToList();
            }

            // Get the ordinals of the cards in the board to fill
            List<int> cardsToAddOrdinals = [];
            for (int i = 1; i <= 16; i++)
            {
                if (!CurrentBoard.BoardCards.Select(e => e.Ordinal).Contains(i))
                    cardsToAddOrdinals.Add(i);
            }

            Random random = new();
            foreach (var ordinal in cardsToAddOrdinals)
            {
                int randomIndex = random.Next(0, availableLotteryCards.Count);
                LotteryCard randomCard = availableLotteryCards[randomIndex];
                CurrentBoard.BoardCards.Add(new BoardCard { CardID = randomCard.CardID, Ordinal = ordinal });
                availableLotteryCards.RemoveAt(randomIndex);
            }

            StateHasChanged();

        }

        private void CleanBoard()
        {
            CurrentBoard.BoardCards = [];
            if (CurrentBoard.HasDoubleCard)
            {
                CurrentBoard.BoardCards.Add(new BoardCard { CardID = CurrentBoard.DoubleCardId, Ordinal = CurrentBoard.DoubleCardPosition.Item1 });
                CurrentBoard.BoardCards.Add(new BoardCard { CardID = CurrentBoard.DoubleCardId, Ordinal = CurrentBoard.DoubleCardPosition.Item2 });
            }
        }
    }
}
