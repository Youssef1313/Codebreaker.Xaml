using Codebreaker.GameAPIs.Client.Models;


namespace CodeBreaker.ViewModels.Components;

public class MoveViewModel
{
    private readonly Move _move;

    public MoveViewModel(Move move) =>
        _move = move;

    public int MoveNumber => _move.MoveNumber;

    public IReadOnlyList<string> GuessPegs => _move.GuessPegs;

    public string[] KeyPegs => _move.KeyPegs;
}
