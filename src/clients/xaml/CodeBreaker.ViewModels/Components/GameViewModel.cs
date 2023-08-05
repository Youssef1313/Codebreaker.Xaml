using System.Collections.ObjectModel;

using CodeBreaker.ViewModels.Models;

namespace CodeBreaker.ViewModels.Components;

public class GameViewModel
{
    private readonly Game _game;

    public GameViewModel(Game game)
    {
        _game = game;

        ColorList = new List<string>(_game.FieldValues["colors"]);
    }

    public Guid GameId => _game.GameId;

    public string Name => _game.PlayerName;

    public string GameType => _game.GameType;

    public IReadOnlyList<string> ColorList { get; private set; }

    public int NumberCodes => _game.NumberCodes;

    public int MaxMoves => _game.MaxMoves;

    public DateTime StartTime => _game.StartTime;

    public ObservableCollection<MoveViewModel> Moves { get; } = new ObservableCollection<MoveViewModel>();
}
