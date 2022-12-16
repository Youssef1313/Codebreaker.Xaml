using CodeBreaker.Services.Authentication;
using CodeBreaker.ViewModels;
using CodeBreaker.WinUI.Contracts.Services;

namespace CodeBreaker.WinUI.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService;

    private readonly IAuthService _authService;

    public DefaultActivationHandler(INavigationService navigationService, IAuthService authService)
    {
        _navigationService = navigationService;
        _authService = authService;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs? args)
    {
        // None of the ActivationHandlers has handled the activation.
        return _navigationService.Frame.Content == null;
    }

    protected override Task HandleInternalAsync(LaunchActivatedEventArgs? args)
    {
        Type pageType = _authService.IsAuthenticated
            ? typeof(GamePageViewModel)
            : typeof(AuthPageViewModel);

        _navigationService.NavigateToViewModel(pageType, args?.Arguments);

        return Task.CompletedTask;
    }
}
