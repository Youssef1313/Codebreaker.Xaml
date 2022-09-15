using System.Collections.ObjectModel;
using CodeBreaker.Services;
using CodeBreaker.Shared.Models.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeBreaker.ViewModels;

[ObservableObject]
public partial class LivePageViewModel
{
    private readonly LiveClient _liveClient;

    public LivePageViewModel(LiveClient liveClient)
    {
        _liveClient = liveClient;
        _liveClient.OnGameEvent += (sender, args) =>
        {
            if (args.Data is null) return;
            Games.Add(new GameViewModel(args.Data));
        };
        _liveClient.OnMoveEvent += (sender, args) =>
        {
            if (args.Data is null) return;
            Move move = args.Data;
            GameViewModel? game = Games.Where(x => x.GameId == args.GameId).SingleOrDefault();
            game?.Moves.Add(new MoveViewModel(move));
        };
    }

    public ObservableCollection<GameViewModel> Games { get; private init; } = new();


    [RelayCommand(AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true, IncludeCancelCommand = true)]
    public async Task StartStreamingAsync(CancellationToken token = default)
    {
        await _liveClient.StartAsync(token);
    }
}
