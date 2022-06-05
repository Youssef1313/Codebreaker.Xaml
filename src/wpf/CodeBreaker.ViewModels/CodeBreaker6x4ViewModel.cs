using CodeBreaker.ViewModels.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;

namespace CodeBreaker.ViewModels;

[ObservableObject]
public partial class CodeBreaker6x4ViewModel
{
    private readonly GameClient _client;

    private int _moveNumber = 0;
    private string _gameId = string.Empty;
    public CodeBreaker6x4ViewModel(GameClient client)
    {
        _client = client;
        ColorList.Add("first");
    }

    [ObservableProperty]
    private string _name = "default user";

    public ObservableCollection<string> ColorList { get; } = new();

    public ObservableCollection<SelectionAndKeyPegs> GameMoves { get; } = new();

    [ICommand]
    public async Task StartGameAsync()
    {
        try
        {
            var response = await _client.StartGameAsync(_name);

            _gameId = response.Id;
            (_, int maxMoves, string[] colors) = response.GameOptions;
            _moveNumber++;

            foreach (var color in colors)
            {
                ColorList.Add(color);
            }
        }
        catch (Exception ex)
        {

        }
    }

    [ICommand]
    public async Task SetMoveAsync()
    {
        string[] selection = { _selectedColor1, _selectedColor2, _selectedColor3, _selectedColor4 };
        var keyPegColors = await _client.SetMoveAsync(_gameId, _moveNumber++, selection);

        GameMoves.Add(new SelectionAndKeyPegs(selection, keyPegColors));
    }

    [ObservableProperty]
    private string _selectedColor1 = string.Empty;

    [ObservableProperty]
    private string _selectedColor2 = string.Empty;

    [ObservableProperty]
    private string _selectedColor3 = string.Empty;

    [ObservableProperty]
    private string _selectedColor4 = string.Empty;

}

public record SelectionAndKeyPegs(string[] Selection, string[] KeyPegs);