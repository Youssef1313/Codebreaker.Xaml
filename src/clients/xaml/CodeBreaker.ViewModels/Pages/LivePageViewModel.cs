using System.Collections.ObjectModel;
using CodeBreaker.Services;
using CodeBreaker.Shared.Models.Data;
using CodeBreaker.ViewModels.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeBreaker.ViewModels;

[ObservableObject]
public partial class LivePageViewModel
{
    private readonly LiveClient _liveClient;

    public LivePageViewModel(LiveClient liveClient)
    {
        SynchronizationContext? context = SynchronizationContext.Current; // The context of the UI-thread.
        _liveClient = liveClient;
        _liveClient.OnGameEvent += (sender, args) =>
        {
            if (args.Data?.Game is null) return;
            context?.Post(_ => Games.Add(new GameViewModel(args.Data)), null);
        };
        _liveClient.OnMoveEvent += (sender, args) =>
        {
            if (args.Data?.Move is null) return;
            Move move = args.Data;
            GameViewModel? game = Games.Where(x => x.GameId == args.GameId).SingleOrDefault();
            context?.Post(_ => game?.Moves.Add(new MoveViewModel(move)), null);
        };
    }

    public ObservableCollection<GameViewModel> Games { get; private init; } = new();


    [RelayCommand(AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true, IncludeCancelCommand = true)]
    public async Task StartStreamingAsync(CancellationToken token = default)
    {
        await _liveClient.StartAsync(token);
    }
}
