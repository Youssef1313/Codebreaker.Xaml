using CommunityToolkit.Mvvm.ComponentModel;
using CodeBreaker.Services;
using CommunityToolkit.Mvvm.Input;
using CodeBreaker.ViewModels.Services;
using Microsoft.Extensions.Logging;
using CodeBreaker.Services.Authentication;

namespace CodeBreaker.ViewModels;

[ObservableObject]
public partial class AuthPageViewModel
{
    private readonly IAuthService _authService;

    private readonly IDialogService _dialogService;

    private readonly ILogger _logger;

    private readonly INavigationServiceCore _navigationService;

    public AuthPageViewModel(IAuthService authService, IDialogService dialogService, ILogger<AuthPageViewModel> logger, INavigationServiceCore navigationService)
    {
        _authService = authService;
        _dialogService = dialogService;
        _logger = logger;
        _navigationService = navigationService;
    }

    [RelayCommand(AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task LoginAsync(CancellationToken cancellation = default)
    {
        try
        {
            await _authService.LoginAsync(cancellation);
        }
        catch (Exception)
        {
            await _dialogService.ShowMessageAsync("Could not authenticate");
            return;
        }

        _navigationService.NavigateTo(typeof(CodeBreaker6x4ViewModel).FullName!, clearNavigation: true);
    }

    [RelayCommand]
    private void ContinueAsGuest() =>
        _navigationService.NavigateTo(typeof(CodeBreaker6x4ViewModel).FullName!, clearNavigation: true);
}
