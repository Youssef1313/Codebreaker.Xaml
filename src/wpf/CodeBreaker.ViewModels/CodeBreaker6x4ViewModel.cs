using CodeBreaker.ViewModels.Services;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeBreaker.ViewModels;

[ObservableObject]
public partial class CodeBreaker6x4ViewModel
{
    private readonly GameClient _client;
    public CodeBreaker6x4ViewModel(GameClient client)
    {
        _client = client;
    }

    [ObservableProperty]
    private string _name = "default user";

    [ICommand]
    public async Task StartGameAsync()
    {
        await _client.StartGameAsync(_name);
    }


}