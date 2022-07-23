using System.Collections.ObjectModel;
using CodeBreaker.LiveService.Shared;
using CodeBreaker.Services;
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
    }

    public ObservableCollection<LiveHubArgsViewModel> LiveEvents { get; private init; } = new ObservableCollection<LiveHubArgsViewModel>();

    [ICommand]
    public async Task StartStreamingAsync(CancellationToken token = default)
    {
        await _liveClient.StartAsync(token);

        await foreach (LiveHubArgs e in _liveClient.SubscribeToEventsAsync(token))
            LiveEvents.Add(new LiveHubArgsViewModel(e));
    }
}
