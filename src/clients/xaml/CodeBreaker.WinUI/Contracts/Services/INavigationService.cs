using CodeBreaker.Services;

namespace CodeBreaker.WinUI.Contracts.Services;

public interface INavigationService : IViewModelNavigationService
{
    event NavigatedEventHandler Navigated;

    bool CanGoBack { get; }

    Frame Frame { get; set; }

    bool NavigateToView(Type pageType, object? parameter = default, bool clearNavigation = false);

    bool NavigateToView(string pageKey, object? parameter = default, bool clearNavigation = false);
}
