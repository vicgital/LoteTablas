using Microsoft.AspNetCore.Components;

namespace LoteTablas.Blazor.UI.Components
{
    public partial class SelectDoubleCardComponent
    {
        [Parameter]
        public EventCallback<Tuple<int, int>> OnDoubleCardSelected { get; set; }

        [Parameter]
        public string ImageUrl { get; set; } = string.Empty;

        [Parameter]
        public Tuple<int, int> DoubleCardPosition { get; set; } = Tuple.Create(0, 0);

        private async Task SelectDoubleCardPosition(int position)
        {
            if (DoubleCardPosition.Item1 > 0 && DoubleCardPosition.Item1 == position)
                return;

            if (DoubleCardPosition.Item1 > 0 & DoubleCardPosition.Item2 > 0)
                DoubleCardPosition = Tuple.Create(position, 0);
            else if (DoubleCardPosition.Item1 == 0)
                DoubleCardPosition = Tuple.Create(position, DoubleCardPosition.Item2);
            else
                DoubleCardPosition = Tuple.Create(DoubleCardPosition.Item1, position);

            //StateHasChanged();
            await OnDoubleCardSelected.InvokeAsync(new Tuple<int, int>(DoubleCardPosition.Item1, DoubleCardPosition.Item2));

        }

    }
}
