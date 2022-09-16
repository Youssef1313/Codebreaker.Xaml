using CodeBreaker.ViewModels;
using CodeBreaker.WinUI.Contracts.Services;
using CodeBreaker.WinUI.ViewModels;

namespace CodeBreaker.WinUI.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService;

    public DefaultActivationHandler(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs? args)
    {
        // None of the ActivationHandlers has handled the activation.
        return _navigationService.Frame.Content == null;
    }

    protected override async Task HandleInternalAsync(LaunchActivatedEventArgs? args)
    {
        string? fullName = typeof(AuthPageViewModel).FullName;
        if (fullName is null) throw new InvalidOperationException();
        
        _navigationService.NavigateTo(fullName, args?.Arguments);

        await Task.CompletedTask;
    }
}
