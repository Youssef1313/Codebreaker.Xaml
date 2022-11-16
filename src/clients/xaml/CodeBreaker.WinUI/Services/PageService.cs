using CodeBreaker.ViewModels;
using CodeBreaker.WinUI.Contracts.Services;
using CodeBreaker.WinUI.Views.Pages;

namespace CodeBreaker.WinUI.Services;

public class PageService : IPageService
{
    /// <summary>
    /// Maps the type name of the viewmodel to the corresponding page type.
    /// </summary>
    private readonly Dictionary<string, Type> _viewModelTypeNameToPageType = new();

    /// <summary>
    /// Maps the type of the viewmodel to the corresponding page type.
    /// </summary>
    private readonly Dictionary<Type, Type> _viewModelTypeToPageType = new();

    /// <summary>
    /// Maps the name of the page to its own type.
    /// </summary>
    private readonly Dictionary<string, Type> _pageTypeNameToPageType = new();

    public PageService()
    {
        Configure<AuthPageViewModel, AuthPage>();
        Configure<GamePageViewModel, GamePage>();
        Configure<LivePageViewModel, LivePage>();
        Configure<SettingsPage>(); // page without viewmodel
    }

    /// <summary>
    /// Get the page type by specifying the type name of the corresponding viewmodel.
    /// </summary>
    /// <param name="viewModelKey">The type name of the corresponding viewmodel</param>
    /// <returns>The page type.</returns>
    /// <exception cref="ArgumentException">Thrown if the page type is not found.</exception>
    public Type GetPageTypeByViewModel(string viewModelKey)
    {
        Type? pageType;

        lock (_viewModelTypeNameToPageType)
        {
            if (!_viewModelTypeNameToPageType.TryGetValue(viewModelKey, out pageType))
                throw new ArgumentException($"Page not found: {viewModelKey}. Did you forget to call PageService.Configure?");
        }

        return pageType;
    }

    /// <summary>
    /// Get the page type by specifying the type of the corresponding viewmodel.
    /// </summary>
    /// <param name="viewModelType">Type tyoe of the corresponding viewmodel.</param>
    /// <returns>The page tyoe.</returns>
    /// <exception cref="ArgumentException">Thrown if the page type is not found.</exception>
    public Type GetPageTypeByViewModel(Type viewModelType)
    {
        Type? pageType;

        lock (_viewModelTypeToPageType)
        {
            if (!_viewModelTypeToPageType.TryGetValue(viewModelType, out pageType))
                throw new ArgumentException($"Page not found: {viewModelType}. Did you forget to call PageService.Configure?");
        }

        return pageType;
    }

    /// <summary>
    /// Get the page type by specifying the name of the page itself.
    /// </summary>
    /// <param name="pageTypeName">The name of the page.</param>
    /// <returns>The page type.</returns>
    /// <exception cref="ArgumentException">Thrown if the page type is not found.</exception>
    public Type GetPageTypeByPageName(string pageTypeName)
    {
        Type? pageType;

        lock (_pageTypeNameToPageType)
        {
            if (!_pageTypeNameToPageType.TryGetValue(pageTypeName, out pageType))
                throw new ArgumentException($"Page not found: {pageTypeName}. Did you forget to call PageService.Configure?");
        }

        return pageType;
    }

    /// <summary>
    /// Maps the viewmodel name to page type, viewmodel type to page type and page name to page type.
    /// </summary>
    /// <typeparam name="VM">The type of the viewmodel.</typeparam>
    /// <typeparam name="V">The type of the page.</typeparam>
    /// <exception cref="InvalidOperationException">Thrown if the type or its name is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the key or the type is already mapped.</exception>
    private void Configure<VM, V>()
        where VM : class
        where V : Page
    {
        Type? vmType = typeof(VM);
        Type? pageType = typeof(V);
        string? vmTypeKey = vmType?.FullName;

        if (vmTypeKey is null || /* just to satisfy compiler warnings -> */ vmType is null)
            throw new InvalidOperationException();

        // VM name -> page type
        lock (_viewModelTypeNameToPageType)
        {
            if (_viewModelTypeNameToPageType.ContainsKey(vmTypeKey))
                throw new ArgumentException($"The key {vmTypeKey} is already configured in {nameof(PageService)}");

            if (_viewModelTypeNameToPageType.Any(p => p.Value == pageType))
                throw new ArgumentException($"This type is already configured with key {_viewModelTypeNameToPageType.First(p => p.Value == pageType).Key}");

            _viewModelTypeNameToPageType.Add(vmTypeKey, pageType);
        }

        // VM type -> page type
        lock(_viewModelTypeToPageType)
        {
            if (_viewModelTypeToPageType.ContainsKey(vmType))
                throw new ArgumentException($"The type {vmType} is already configured in {nameof(PageService)}");

            if (_viewModelTypeToPageType.Any(p => p.Value == pageType))
                throw new ArgumentException($"This type is already configured with key {_viewModelTypeToPageType.First(p => p.Value == pageType).Key}");

            _viewModelTypeToPageType.Add(vmType, pageType);
        }

        // page name -> page type
        Configure<V>();
    }

    /// <summary>
    /// Maps the page name to the page type.
    /// </summary>
    /// <typeparam name="V">The type of the page.</typeparam>
    /// <exception cref="InvalidOperationException">Thrown if the type or its name is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the key or the type is already mapped.</exception>
    private void Configure<V>()
        where V : Page
    {
        Type? pageType = typeof(V);
        string? pageTypeKey = pageType?.FullName;

        if (pageTypeKey is null || /* just to satisfy compiler warnings -> */ pageType is null)
            throw new InvalidOperationException();

        lock (_pageTypeNameToPageType)
        {
            if (_pageTypeNameToPageType.ContainsKey(pageTypeKey))
                throw new ArgumentException($"The key {pageTypeKey} is already configured in {nameof(PageService)}");

            if (_pageTypeNameToPageType.Any(p => p.Value == pageType))
                throw new ArgumentException($"The type is already configured with key {_pageTypeNameToPageType.First(p => p.Value == pageType).Key}");

            _pageTypeNameToPageType.Add(pageTypeKey, pageType);
        }
    }
}
