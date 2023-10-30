using Codebreaker.ViewModels.Contracts.Services;

namespace CodeBreaker.WinUI.Contracts.Services.Navigation;

public interface IWinUINavigationService : INavigationService
{
    event NavigationFailedEventHandler? NavigationFailed;

    event NavigatedEventHandler? Navigated;

    Frame Frame { get; set; }
}
