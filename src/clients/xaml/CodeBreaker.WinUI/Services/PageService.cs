using CodeBreaker.ViewModels;
using CodeBreaker.WinUI.Contracts.Services;
using CodeBreaker.WinUI.Views.Pages;

namespace CodeBreaker.WinUI.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _viewModelTypeNameToPageType = new();

    private readonly Dictionary<Type, Type> _viewModelTypeToPageType = new();

    public PageService()
    {
        Configure<AuthPageViewModel, AuthPage>();
        Configure<GamePageViewModel, GamePage>();
        Configure<LivePageViewModel, LivePage>();
    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_viewModelTypeNameToPageType)
        {
            if (!_viewModelTypeNameToPageType.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    private void Configure<VM, V>()
        where VM : class
        where V : Page
    {
        lock (_viewModelTypeNameToPageType)
        {
            Type? vmType = typeof(VM);
            string? vmTypeKey = vmType?.FullName;

            if (vmTypeKey is null || /* just to satisfy compiler warnings -> */ vmType is null)
                throw new InvalidOperationException();

            if (_viewModelTypeNameToPageType.ContainsKey(vmTypeKey))
                throw new ArgumentException($"The key {vmTypeKey} is already configured in {nameof(PageService)}");

            if (_viewModelTypeToPageType.ContainsKey(vmType))
                throw new ArgumentException($"The type {vmType} is already configured in {nameof(PageService)}");

            Type? pageType = typeof(V);

            if (_viewModelTypeNameToPageType.Any(p => p.Value == pageType))
                throw new ArgumentException($"This type is already configured with key {_viewModelTypeNameToPageType.First(p => p.Value == pageType).Key}");

            if (_viewModelTypeToPageType.Any(p => p.Value == pageType))
                throw new ArgumentException($"This type is already configured with key {_viewModelTypeToPageType.First(p => p.Value == pageType).Key}");

            _viewModelTypeNameToPageType.Add(vmTypeKey, pageType);
            _viewModelTypeToPageType.Add(vmType, pageType);
        }
    }
}
