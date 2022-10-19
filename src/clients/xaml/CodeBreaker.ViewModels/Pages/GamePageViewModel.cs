using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.Shared.Models.Api;
using CodeBreaker.Shared.Models.Data;
using CodeBreaker.Shared.Models.Extensions;
using CodeBreaker.ViewModels.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;

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

public class GamePageViewModelOptions
{
    public bool EnableDialogs { get; set; } = false;
}

[ObservableObject]
public partial class GamePageViewModel
{
    private readonly IGameClient _client;
    private int _moveNumber = 0;
    private GameDto? _game;
    private readonly bool _enableDialogs = false;
    private readonly IDialogService _dialogService;
    private readonly IAuthService _authService;

    public GamePageViewModel(
        IGameClient client,
        IOptions<GamePageViewModelOptions> options,
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
            if (e.PropertyName == nameof(GameStatus))
                WeakReferenceMessenger.Default.Send(new GameStateChangedMessage(GameStatus));
        };

        SetGamerNameIfAvailable();
    }

    public GameDto? Game
    {
        get => _game;
        set
        {
            OnPropertyChanging(nameof(Game));
            OnPropertyChanging(nameof(Fields));
            _game = value;

            Fields.Clear();

            for (int i = 0; i < value?.Type.Holes; i++)
            {
                SelectedFieldViewModel field = new();
                field.PropertyChanged += (sender, e) => SetMoveCommand.NotifyCanExecuteChanged();
                Fields.Add(field);
            }

            OnPropertyChanged(nameof(Game));
            OnPropertyChanged(nameof(Fields));
        }
    }

    [ObservableProperty]
    private string _name = string.Empty;

    [NotifyPropertyChangedFor(nameof(IsNameEnterable))]
    [ObservableProperty]
    private bool _isNamePredefined = false;

    public ObservableCollection<SelectedFieldViewModel> Fields { get; } = new(); // BindingList does not work here

    public ObservableCollection<SelectionAndKeyPegs> GameMoves { get; } = new();

    [ObservableProperty]
    private GameMode _gameStatus = GameMode.NotRunning;

    [NotifyPropertyChangedFor(nameof(IsNameEnterable))]
    [ObservableProperty]
    private bool _inProgress = false;

    [ObservableProperty]
    private bool _isCancelling = false;

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

            Game = response.Game;
            _moveNumber++;
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

    [RelayCommand(AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task CancelGameAsync()
    {
        if (Game is null)
            throw new InvalidOperationException("No game running");

        IsCancelling = true;

        try
        {
            await _client.CancelGameAsync(Game!.Value.GameId);
            GameStatus = GameMode.NotRunning;
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
            IsCancelling = false;
        }
    }

    [RelayCommand(CanExecute = nameof(CanSetMove), AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task SetMoveAsync()
    {
        try
        {
            InProgress = true;
            WeakReferenceMessenger.Default.Send(new GameMoveMessage(GameMoveValue.Started));

            if (_game is null)
                throw new InvalidOperationException("no game running");

            if (Fields.Count != _game.Value.Type.Holes || Fields.Any(x => !x.IsSet))
                throw new InvalidOperationException("all colors need to be selected before invoking this method");

            string[] selection = Fields.Select(x => x.Value!).ToArray();

            CreateMoveResponse response = await _client.SetMoveAsync(_game.Value.GameId, selection);

            SelectionAndKeyPegs selectionAndKeyPegs = new(selection, response.KeyPegs, _moveNumber++);
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
        Fields.All(s => s is not null && s.IsSet);

    private void ClearSelectedColor()
    {
        for (int i = 0; i < Fields.Count; i++)
            Fields[i].Reset();

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

    private void InitializeValues()
    {
        ClearSelectedColor();
        GameMoves.Clear();
        GameStatus = GameMode.NotRunning;
        ErrorMessage.IsVisible = false;
        ErrorMessage.Message = string.Empty;
        InfoMessage.IsVisible = false;
        InfoMessage.Message = string.Empty;
        _moveNumber = 0;
    }

    public InfoMessageViewModel ErrorMessage { get; } = new InfoMessageViewModel { IsError = true, Title = "Error" };

    public InfoMessageViewModel InfoMessage { get; }
}

public record SelectionAndKeyPegs(string[] GuessPegs, KeyPegsDto KeyPegs, int MoveNumber);

public record class GameStateChangedMessage(GameMode GameMode);

public record class GameMoveMessage(GameMoveValue GameMoveValue, SelectionAndKeyPegs? SelectionAndKeyPegs = null);
