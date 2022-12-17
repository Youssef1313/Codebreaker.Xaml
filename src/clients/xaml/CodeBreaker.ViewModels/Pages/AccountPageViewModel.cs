using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeBreaker.ViewModels.Pages;

[ObservableObject]
public partial class AccountPageViewModel
{
    private readonly IAuthService _authService;

    private readonly IViewModelNavigationService _navigationService;

    public AccountPageViewModel(IAuthService authService, IViewModelNavigationService navigationService)
    {
        _authService = authService;
        _navigationService = navigationService;
    }

    [RelayCommand(AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        await _authService.LogoutAsync(cancellationToken);
        _navigationService.NavigateToViewModel(typeof(GamePageViewModel), clearNavigation: true);
    }
}
