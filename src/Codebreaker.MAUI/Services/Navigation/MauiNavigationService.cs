using Codebreaker.MAUI.Contracts.Services.Navigation;
using Codebreaker.MAUI.DataStructures;
using Codebreaker.ViewModels.Contracts.Services;

namespace Codebreaker.MAUI.Services.Navigation;

internal class MauiNavigationService : INavigationService
{
    private readonly CircularStack<string> _navigationStack = new(1000);

    private readonly IPageService _pageService;

    public MauiNavigationService(IPageService pageService)
    {
        _navigationStack.Push(Shell.Current.CurrentState.Location.ToString()); // Put the initial page on the stack
        _pageService = pageService;
    }

    public bool CanGoBack => !_navigationStack.IsEmpty;

    public async ValueTask<bool> GoBackAsync()
    {
        if (_navigationStack.TryPop(out _) && _navigationStack.TryPeek(out var route))
            return await Shell.Current.GoToAsync(route);

        return false;
    }

    public async ValueTask<bool> NavigateToAsync(string key, object? parameter = null, bool clearNavigation = false)
    {
        string shellRoute = _pageService[key];
        await Shell.Current.GoToAsync(shellRoute, new Dictionary<string, object?>
        {
            { "param", parameter }
        });

        if (clearNavigation)
            _navigationStack.Clear();

        _navigationStack.Push(key);
        return true;
    }
}
