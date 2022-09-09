using CodeBreaker.Shared.Models.Data;

namespace CodeBreaker.ViewModels;

public class MoveViewModel
{
    private readonly Move _move;

    public MoveViewModel(Move move) =>
        _move = move;

    public int MoveNumber => _move.MoveNumber;

    public IReadOnlyList<string> GuessPegs => _move.GuessPegs;

    public KeyPegs? KeyPegs => _move.KeyPegs;
}
