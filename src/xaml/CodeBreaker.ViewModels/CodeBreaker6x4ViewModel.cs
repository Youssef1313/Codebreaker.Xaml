using CodeBreaker.Services;
using CodeBreaker.ViewModels.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.Options;

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
    
    public CodeBreaker6x4ViewModel(
        IGameClient client, 
        IOptions<CodeBreaker6x4ViewModelOptions> options,
        IDialogService dialogService)
    {
        _client = client;
        _dialogService = dialogService;
        _enableDialogs = options.Value.EnableDialogs;
        
        SetMoveCommand = new AsyncRelayCommand(SetMoveAsync, CanSetMove);
        InfoMessage = new InfoMessageViewModel
        {
            IsError = false,
            Title = "Information",
            ActionTitle = "Weiter",
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
    }

    [ObservableProperty]
    private string _name = string.Empty;

    public ObservableCollection<string> ColorList { get; } = new();

    public ObservableCollection<SelectionAndKeyPegs> GameMoves { get; } = new();

    [ObservableProperty]
    private GameMode _gameStatus = GameMode.NotRunning;

    [NotifyPropertyChangedFor(nameof(IsEnabled))]
    [ObservableProperty]
    private bool _inProgress = false;

    public bool IsEnabled => !InProgress;

    [RelayCommand]
    private async Task StartGameAsync()
    {
        try
        {
            InitializeValues();

            InProgress = true;
            var response = await _client.StartGameAsync(_name);

            GameStatus = GameMode.Started;

            _gameId = response.Id;
            (_, _, int maxMoves, string[] colors) = response.GameOptions;
            _moveNumber++;

            ColorList.Clear();

            foreach (string color in colors)
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

    public AsyncRelayCommand SetMoveCommand { get; }

    private async Task SetMoveAsync()
    {
        try
        {
            InProgress = true;
            WeakReferenceMessenger.Default.Send(new GameMoveMessage(GameMoveValue.Started));

            if (_selectedColor1 is null || _selectedColor2 is null || _selectedColor3 is null || _selectedColor4 is null)
                throw new InvalidOperationException("all colors need to be selected before invoking this method");

            string[] selection = { _selectedColor1, _selectedColor2, _selectedColor3, _selectedColor4 };

            (bool completed, bool won, string[] keyPegColors) = await _client.SetMoveAsync(_gameId, _moveNumber, selection);

            SelectionAndKeyPegs selectionAndKeyPegs = new(selection, keyPegColors, _moveNumber++);
            GameMoves.Add(selectionAndKeyPegs);
            GameStatus = GameMode.MoveSet;

            WeakReferenceMessenger.Default.Send(new GameMoveMessage(GameMoveValue.Completed, selectionAndKeyPegs));

            if (won)
            {
                GameStatus = GameMode.Won;
                InfoMessage.Message = "Congratulations - you won!";
                InfoMessage.IsVisible = true;
                if (_enableDialogs)
                {
                    await _dialogService.ShowMessageAsync(InfoMessage.Message);
                }
            }
            else if (completed)
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
            {
                await _dialogService.ShowMessageAsync(ErrorMessage.Message);
            }
        }
        finally
        {
            ClearSelectedColor();
            InProgress = false;
        }
    }

    private bool CanSetMove()
    {
        string?[] selections = { _selectedColor1, _selectedColor2, _selectedColor3, _selectedColor4 };
        return selections.All(s => s is not null);
    }

    private void ClearSelectedColor()
    {
        SelectedColor1 = null;
        SelectedColor2 = null;
        SelectedColor3 = null;
        SelectedColor4 = null;

        SetMoveCommand.NotifyCanExecuteChanged();
    }

    private string? _selectedColor1 = null;
    public string? SelectedColor1
    {
        get => _selectedColor1;
        set
        {
            if (SetProperty(ref _selectedColor1, value))
                SetMoveCommand.NotifyCanExecuteChanged();
        }
    }

    private string? _selectedColor2 = null;
    public string? SelectedColor2
    {
        get => _selectedColor2;
        set
        {
            if (SetProperty(ref _selectedColor2, value))
                SetMoveCommand.NotifyCanExecuteChanged();
        }
    }

    private string? _selectedColor3 = null;
    public string? SelectedColor3
    {
        get => _selectedColor3;
        set
        {
            if (SetProperty(ref _selectedColor3, value))
                SetMoveCommand.NotifyCanExecuteChanged();
        }
    }

    private string? _selectedColor4 = null;
    public string? SelectedColor4
    {
        get => _selectedColor4;
        set
        {
            if (SetProperty(ref _selectedColor4, value))
                SetMoveCommand.NotifyCanExecuteChanged();
        }
    }

    private readonly string[] _selectedColorPropertyNames = { nameof(SelectedColor1), nameof(SelectedColor2), nameof(SelectedColor3), nameof(SelectedColor4) };

    public InfoMessageViewModel ErrorMessage { get; } = new InfoMessageViewModel { IsError = true, Title = "Error" };

    public InfoMessageViewModel InfoMessage { get; }
}

public record SelectionAndKeyPegs(string[] Selection, string[] KeyPegs, int MoveNumber);

public record class GameStateChangedMessage(GameMode GameMode);

public record class GameMoveMessage(GameMoveValue GameMoveValue, SelectionAndKeyPegs? SelectionAndKeyPegs = null);
