using BlazorAnimate;
using BlazorBootstrap;
using LoteTablas.Blazor.UI.Data.Repositories.Definition;
using LoteTablas.Blazor.UI.Enums;
using LoteTablas.Blazor.UI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LoteTablas.Blazor.UI.Pages
{
    public partial class Home
    {
        [Inject]
        ILogger<Home> Logger { get; set; } = default!;

        [Inject]
        ILotteryRepository LotteryRepository { get; set; } = default!;

        [Inject]
        IBoardRepository BoardRepository { get; set; } = default!;

        [Inject]
        IJSRuntime JS { get; set; } = default!;

        [Inject]
        protected PreloadService PreloadService { get; set; } = default!;

        private List<ToastMessage> ToastMessages { get; set; } = [];

        private Modal SelectCardModal { get; set; } = default!;

        private List<LotteryCard> LotteryCards { get; set; } = [];

        private List<Board> UserBoards { get; set; } = [];

        private Board? CurrentBoard { get; set; } = null;

        private int CurrentBoardId { get; set; } = 1;

        private CreateBoardStep CurrentStep { get; set; } = CreateBoardStep.Start;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShowLoader();
                UserBoards = new List<Board>(4);
                LotteryCards = await LotteryRepository.GetLotteryCardsByLotteryID(1);
                await base.OnInitializedAsync();
            }
            catch (Exception ex)
            {
                ShowToast("Ocurrió un error al cargar la página", ToastType.Danger);
                Logger.LogError(ex, "OnInitializedAsync");
            }
            finally
            {
                HideLoader();
            }

        }

        private void Start()
        {
            CurrentBoard = new Board(CurrentBoardId);
            CurrentStep = CreateBoardStep.SelectDouble;
        }

        private void SelectDoubleCardCompleted(Board board)
        {
            CurrentBoard = board;
            CurrentStep = CreateBoardStep.AddCards;
        }

        private void AddCardsCompleted(Board board)
        {
            if (UserBoards.Any(b => b.BoardId == board.BoardId))
            {
                // it's an edit
                var index = UserBoards.FindIndex(b => b.BoardId == board.BoardId);
                UserBoards[index] = board;
            }
            else
            {
                // it's a new board
                UserBoards.Add(board);
            }
            CurrentBoard = null;
            CurrentStep = CreateBoardStep.DownloadBoards;
            ShowToast("Tabla agregada..", ToastType.Success);
        }

        private void AddNewBoard()
        {
            CurrentBoardId++;
            CurrentBoard = new Board(CurrentBoardId);
            CurrentStep = CreateBoardStep.SelectDouble;
        }

        private void DeleteBoard(Board board)
        {
            UserBoards.Remove(board);
        }

        private void EditBoard(Board board)
        {
            CurrentBoard = UserBoards.FirstOrDefault(b => b.BoardId == board.BoardId);
            CurrentStep = CreateBoardStep.AddCards;
        }

        private async Task DownloadBoards()
        {
            try
            {
                ShowLoader("Generando tablas..");
                var stream = await BoardRepository.DownloadBoards(UserBoards);
                var fileName = $"lotetablas_{DateTime.Now.ToFileTimeUtc()}.pdf";

                using var streamRef = new DotNetStreamReference(stream: stream);
                await JS.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
                ShowToast("Tus tablas han sido generadas!", ToastType.Success);
            }
            catch (Exception ex)
            {
                ShowToast("Ocurrió un error al descargar tus tablas, intenta de nuevo.", ToastType.Danger);
                Logger.LogError(ex, "OnInitializedAsync");
            }
            finally
            {
                HideLoader();
            }
        }


        private void ShowLoader(string message = "Cargando...") =>
            PreloadService.Show(SpinnerColor.Light, message);

        private void HideLoader() =>
            PreloadService.Hide();

        private void ShowToast(string message, ToastType toastType) =>
            ToastMessages.Add(new ToastMessage
            {
                Type = toastType,
                Message = message
            });

        private void Back()
        {
            switch (CurrentStep)
            {
                case CreateBoardStep.SelectDouble:
                    break;
                case CreateBoardStep.AddCards:
                    CurrentStep = CreateBoardStep.SelectDouble;
                    break;
                case CreateBoardStep.DownloadBoards:
                    CurrentStep = CreateBoardStep.AddCards;
                    break;
                default:
                    break;
            }
        }



    }
}
