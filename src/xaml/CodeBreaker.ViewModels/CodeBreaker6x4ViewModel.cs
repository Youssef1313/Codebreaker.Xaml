using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.Shared.Models.Api;
using CodeBreaker.Shared.Models.Data;
using CodeBreaker.ViewModels.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CodeBreaker.ViewModels;

public enum GameMode
{
    NotRunning,
    Started,
    MoveSet,
    Lost,
    Won
}

public enum GameMoveValue
{
    Started,
    Completed
}

public class CodeBreaker6x4ViewModelOptions
{
    public bool EnableDialogs { get; set; } = false;
}

[ObservableObject]
public partial class CodeBreaker6x4ViewModel
{
    private readonly IGameClient _client;

    private int _moveNumber = 0;
    private Guid _gameId = Guid.Empty;
    private readonly bool _enableDialogs = false;
    private readonly IDialogService _dialogService;
    private readonly IAuthService _authService;
    public CodeBreaker6x4ViewModel(
        IGameClient client,
        IOptions<CodeBreaker6x4ViewModelOptions> options,
        IDialogService dialogService,
        IAuthService authService)
    {
        _client = client;
        _dialogService = dialogService;
        _authService = authService;
        _enableDialogs = options.Value.EnableDialogs;

        InfoMessage = new InfoMessageViewModel
        {
            IsError = false,
            Title = "Information",
            ActionTitle = "Continue",
            ActionCommand = new RelayCommand(() =>
            {
                GameStatus = GameMode.NotRunning;
                InfoMessage!.IsVisible = false;
            })
        };

        PropertyChanged += (sender, e) =>
        {
            if (_selectedColorPropertyNames.Contains(e.PropertyName))
                SetMoveCommand.NotifyCanExecuteChanged();

            if (e.PropertyName == nameof(GameStatus))
                WeakReferenceMessenger.Default.Send(new GameStateChangedMessage(GameStatus));
        };

        SetGamerNameIfAvailable();
    }

    [ObservableProperty]
    private string _name = string.Empty;

    [NotifyPropertyChangedFor(nameof(IsNameEnterable))]
    [ObservableProperty]
    private bool _isNamePredefined = false;

    public ObservableCollection<string> ColorList { get; } = new();

    public ObservableCollection<SelectionAndKeyPegs> GameMoves { get; } = new();

    [ObservableProperty]
    private GameMode _gameStatus = GameMode.NotRunning;

    [NotifyPropertyChangedFor(nameof(IsNameEnterable))]
    [ObservableProperty]
    private bool _inProgress = false;

    public bool IsNameEnterable => !InProgress && !_isNamePredefined;

    [RelayCommand(AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task StartGameAsync()
    {
        try
        {
            InitializeValues();

            InProgress = true;
            CreateGameResponse response = await _client.StartGameAsync(_name, "6x4Game");

            GameStatus = GameMode.Started;

            _gameId = response.Game.GameId;
            _moveNumber++;

            ColorList.Clear();

            foreach (string color in response.Game.Type.Fields)
                ColorList.Add(color);
        }
        catch (Exception ex)
        {
            ErrorMessage.IsVisible = true;
            ErrorMessage.Message = ex.Message;
            if (_enableDialogs)
            {
                await _dialogService.ShowMessageAsync(ErrorMessage.Message);
            }
        }
        finally
        {
            InProgress = false;
        }
    }

    private void InitializeValues()
    {
        ClearSelectedColor();
        GameMoves.Clear();
        ColorList.Clear();
        GameStatus = GameMode.NotRunning;
        ErrorMessage.IsVisible = false;
        ErrorMessage.Message = string.Empty;
        InfoMessage.IsVisible = false;
        InfoMessage.Message = string.Empty;
        _moveNumber = 0;
    }

    [RelayCommand(CanExecute = nameof(CanSetMove), AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task SetMoveAsync()
    {
        try
        {
            InProgress = true;
            WeakReferenceMessenger.Default.Send(new GameMoveMessage(GameMoveValue.Started));

            if (_selectedColor1 is null || _selectedColor2 is null || _selectedColor3 is null || _selectedColor4 is null)
                throw new InvalidOperationException("all colors need to be selected before invoking this method");

            string[] selection = { _selectedColor1, _selectedColor2, _selectedColor3, _selectedColor4 };

            CreateMoveResponse response = await _client.SetMoveAsync(_gameId, selection);

            SelectionAndKeyPegs selectionAndKeyPegs = new(selection, response.KeyPegs.ToModel(), _moveNumber++);
            GameMoves.Add(selectionAndKeyPegs);
            GameStatus = GameMode.MoveSet;

            WeakReferenceMessenger.Default.Send(new GameMoveMessage(GameMoveValue.Completed, selectionAndKeyPegs));

            if (response.Won)
            {
                GameStatus = GameMode.Won;
                InfoMessage.Message = "Congratulations - you won!";
                InfoMessage.IsVisible = true;
                if (_enableDialogs)
                {
                    await _dialogService.ShowMessageAsync(InfoMessage.Message);
                }
            }
            else if (response.Ended)
            {
                GameStatus = GameMode.Lost;
                InfoMessage.Message = "Sorry, you didn't find the matching colors!";
                InfoMessage.IsVisible = true;
                if (_enableDialogs)
                {
                    await _dialogService.ShowMessageAsync(InfoMessage.Message);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage.IsVisible = true;
            ErrorMessage.Message = ex.Message;

            if (_enableDialogs)
                await _dialogService.ShowMessageAsync(ErrorMessage.Message);
        }
        finally
        {
            ClearSelectedColor();
            InProgress = false;
        }
    }

    private bool CanSetMove =>
        new[] { _selectedColor1, _selectedColor2, _selectedColor3, _selectedColor4 }.All(s => s is not null);

    private void ClearSelectedColor()
    {
        SelectedColor1 = null;
        SelectedColor2 = null;
        SelectedColor3 = null;
        SelectedColor4 = null;

        SetMoveCommand.NotifyCanExecuteChanged();
    }

    private void SetGamerNameIfAvailable()
    {
        string? gamerName = _authService.LastUserInformation?.GamerName;

        if (string.IsNullOrWhiteSpace(gamerName))
            return;

        Name = gamerName;
        IsNamePredefined = true;
    }

    [ObservableProperty]
    private string? _selectedColor1;

    [ObservableProperty]
    private string? _selectedColor2;

    [ObservableProperty]
    private string? _selectedColor3;

    [ObservableProperty]
    private string? _selectedColor4;

    private readonly string[] _selectedColorPropertyNames = { nameof(SelectedColor1), nameof(SelectedColor2), nameof(SelectedColor3), nameof(SelectedColor4) };

    public InfoMessageViewModel ErrorMessage { get; } = new InfoMessageViewModel { IsError = true, Title = "Error" };

    public InfoMessageViewModel InfoMessage { get; }
}

public record SelectionAndKeyPegs(string[] GuessPegs, KeyPegs KeyPegs, int MoveNumber);

public record class GameStateChangedMessage(GameMode GameMode);

public record class GameMoveMessage(GameMoveValue GameMoveValue, SelectionAndKeyPegs? SelectionAndKeyPegs = null);
