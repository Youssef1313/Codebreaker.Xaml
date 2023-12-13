using Codebreaker.ViewModels.Contracts.Services;

namespace Codebreaker.MAUI.Services.Navigation;

internal class MauiNavigationService : INavigationService
{
    private readonly Stack<string> _navigationStack = new(1000);

    public MauiNavigationService()
    {
        _navigationStack.Push(Shell.Current.CurrentState.Location.ToString()); // Put the initial page on the stack
    }

    public bool CanGoBack => _navigationStack.Count != 0;

    public async ValueTask<bool> GoBackAsync()
    {
        if (_navigationStack.TryPop(out _) && _navigationStack.TryPeek(out var route))
            await Shell.Current.GoToAsync(route);

        return false;
    }

    public async ValueTask<bool> NavigateToAsync(string key, object? parameter = null, bool clearNavigation = false)
    {
        await Shell.Current.GoToAsync(key, new Dictionary<string, object?>
        {
            { "param", parameter }
        });

        if (clearNavigation)
            _navigationStack.Clear();

        _navigationStack.Push(key);
        // TODO: Check/limit the stack size
        return true;
    }
}
