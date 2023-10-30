using CodeBreaker.WinUI.Contracts.Services.Navigation;

namespace CodeBreaker.WinUI.Activation;

public class DefaultActivationHandler(IWinUINavigationService navigationService) : ActivationHandler<LaunchActivatedEventArgs>
{
    protected override bool CanHandleInternal(LaunchActivatedEventArgs? args)
    {
        // None of the ActivationHandlers has handled the activation.
        return navigationService.Frame.Content == null;
    }

    protected override async Task HandleInternalAsync(LaunchActivatedEventArgs? args)
    {
        await navigationService.NavigateToAsync("GamePage", args?.Arguments);
    }
}
