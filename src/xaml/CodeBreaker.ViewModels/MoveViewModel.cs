using CodeBreaker.Shared;

namespace CodeBreaker.ViewModels;

public class MoveViewModel
{
    private readonly GameMove _move;

    public MoveViewModel(GameMove move) =>
        _move = move;

    public int MoveNumber => _move.MoveNumber;

    public IList<string> GuessPegs => _move.GuessPegs;
}
