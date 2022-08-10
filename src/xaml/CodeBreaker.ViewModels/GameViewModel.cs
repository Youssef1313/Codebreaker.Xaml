using System.Collections.ObjectModel;
using CodeBreaker.Shared;

namespace CodeBreaker.ViewModels;

public class GameViewModel
{
    private readonly Game _game;

    public GameViewModel(Game game)
    {
        _game = game;
    }

    public Guid GameId => _game.GameId;

    public string Name => _game.Name;

    public string GameType => _game.GameType;

    public IReadOnlyList<string> Code => _game.Code;

    public IReadOnlyList<string> ColorList => _game.ColorList;

    public int Holes => _game.Holes;

    public int MaxMoves => _game.MaxMoves;

    public DateTime StartTime => _game.StartTime;

    public ObservableCollection<MoveViewModel> Moves { get; init; } = new ObservableCollection<MoveViewModel>();
}
