using CodeBreaker.Shared.Models.Live;

namespace CodeBreaker.ViewModels;

public class LiveHubArgsViewModel
{
    private readonly LiveHubArgs _model;

    public LiveHubArgsViewModel(LiveHubArgs model)
        => _model = model;

    public string Name => _model.Name;

    public string? Data => _model.Data?.ToString();
}
