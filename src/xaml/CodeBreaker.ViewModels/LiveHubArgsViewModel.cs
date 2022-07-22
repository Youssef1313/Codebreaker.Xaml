using System.Text.Json;
using CodeBreaker.LiveService.Shared;

namespace CodeBreaker.ViewModels;

public class LiveHubArgsViewModel
{
    private readonly LiveHubArgs _model;

    public LiveHubArgsViewModel(LiveHubArgs model)
        => _model = model;

    public string Name => _model.Name;

    public string? Data => _model.Data?.ToString();
}
