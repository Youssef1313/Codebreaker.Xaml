using CodeBreaker.ViewModels.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

[ObservableObject]
public partial class CodeBreaker6x4ViewModel
{
    private readonly GameClient _client;

    private int _moveNumber = 0;
    private string _gameId = string.Empty;
    public CodeBreaker6x4ViewModel(GameClient client)
    {
        _client = client;
        SetMoveCommand = new AsyncRelayCommand(SetMoveAsync, CanSetMove);

        PropertyChanged += (sender, e) =>
        {
            if (_selectedColorPropertyNames.Contains(e.PropertyName))
            {
                SetMoveCommand.NotifyCanExecuteChanged();
            }
        };
    }

    [ObservableProperty]
    private string _name = "default user";

    public ObservableCollection<string> ColorList { get; } = new();

    public ObservableCollection<SelectionAndKeyPegs> GameMoves { get; } = new();

    [ObservableProperty]
    private GameMode _gameStatus = GameMode.NotRunning;

    [AlsoNotifyChangeFor(nameof(IsEnabled))]
    [ObservableProperty]
    private bool _inProgress = false;

    public bool IsEnabled => !InProgress;

    [ICommand]
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

            foreach (var color in colors)
            {               
                ColorList.Add(color);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage.IsVisible = true;
            ErrorMessage.Message = ex.Message;
        }
        finally
        {
            InProgress = false;
        }
    }

    private void InitializeValues()
    {
        SelectedColor1 = string.Empty;
        SelectedColor2 = string.Empty;
        SelectedColor3 = string.Empty;
        SelectedColor4 = string.Empty;
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

    // [ICommand]
    private async Task SetMoveAsync()
    {
        try
        {
            InProgress = true;
            string[] selection = { _selectedColor1, _selectedColor2, _selectedColor3, _selectedColor4 };
            (bool completed, bool won, string[] keyPegColors) = await _client.SetMoveAsync(_gameId, _moveNumber, selection);

            GameMoves.Add(new SelectionAndKeyPegs(selection, keyPegColors, _moveNumber++));
            GameStatus = GameMode.MoveSet;

            if (won)
            {
                GameStatus = GameMode.Won;
                InfoMessage.Message = "Congratulations - you won!";
                InfoMessage.IsVisible = true;
            }
            else if (completed)
            {
                GameStatus = GameMode.Lost;
                InfoMessage.Message = "Sorry, you didn't find the matching colors!";
                InfoMessage.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage.IsVisible = true;
            ErrorMessage.Message = ex.Message;
        }
        finally
        {
            InProgress = false;
        }
    }

    private bool CanSetMove()
    {
        string[] selections = { _selectedColor1, _selectedColor2, _selectedColor3, _selectedColor4 };
        return selections.All(s => s != string.Empty);
    }

    [ObservableProperty]
    private string _selectedColor1 = string.Empty;

    [ObservableProperty]
    private string _selectedColor2 = string.Empty;

    [ObservableProperty]
    private string _selectedColor3 = string.Empty;

    [ObservableProperty]
    private string _selectedColor4 = string.Empty;

    private string[] _selectedColorPropertyNames = { nameof(SelectedColor1), nameof(SelectedColor2), nameof(SelectedColor3), nameof(SelectedColor4) };

    public InfoMessageViewModel ErrorMessage { get; } = new InfoMessageViewModel { IsError = true, Title = "Error" };

    public InfoMessageViewModel InfoMessage { get; } = new InfoMessageViewModel { IsError = false, Title = "Information" };
}

public record SelectionAndKeyPegs(string[] Selection, string[] KeyPegs, int MoveNumber);