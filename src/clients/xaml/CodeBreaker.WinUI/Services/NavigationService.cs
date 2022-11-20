using CodeBreaker.WinUI.Contracts.Services;
using CodeBreaker.WinUI.Contracts.ViewModels;

namespace CodeBreaker.WinUI.Services;

// For more information on navigation between pages see
// https://github.com/microsoft/TemplateStudio/blob/main/docs/WinUI/navigation.md
public class NavigationService : INavigationService
{
    private readonly IPageService _pageService;
    private object? _lastParameterUsed;
    private Frame? _frame;

    public event NavigatedEventHandler? Navigated;

    public Frame Frame
    {
        get
        {
            if (_frame == null)
            {
                _frame = App.MainWindow.Content as Frame ?? throw new InvalidOperationException("MainWindow content is not a frame");
                RegisterFrameEvents();
            }

            return _frame;
        }

        set
        {
            UnregisterFrameEvents();
            _frame = value;
            RegisterFrameEvents();
        }
    }

    public bool CanGoBack => Frame.CanGoBack;

    public NavigationService(IPageService pageService)
    {
        _pageService = pageService;
    }

    private void RegisterFrameEvents()
    {
        if (_frame is not null)
            _frame.Navigated += OnNavigated;
    }

    private void UnregisterFrameEvents()
    {
        if (_frame is not null)
            _frame.Navigated -= OnNavigated;
    }

    public bool GoBack()
    {
        if (!CanGoBack)
            return false;

        object? vmBeforeNavigation = _frame?.GetPageViewModel();
        _frame?.GoBack();

        if (vmBeforeNavigation is INavigationAware navigationAware)
            navigationAware.OnNavigatedFrom();

        return true;
    }

    public bool NavigateToView(Type pageType, object? parameter = default, bool clearNavigation = false)
    {
        if (_frame?.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed)))
        {
            if (_frame is null)
                throw new InvalidOperationException("Frame is null");

            _frame.Tag = clearNavigation;
            object? vmBeforeNavigation = _frame.GetPageViewModel();
            bool navigated = _frame.Navigate(pageType, parameter);      // If the page class has constructor parameters, this method will throw an exception

            if (navigated)
            {
                _lastParameterUsed = parameter;

                if (vmBeforeNavigation is INavigationAware navigationAware)
                    navigationAware.OnNavigatedFrom();
            }

            return navigated;
        }

        return false;
    }

    public bool NavigateToView(string pageKey, object? parameter = default, bool clearNavigation = false)
    {
        Type? pageType = _pageService.GetPageTypeByPageName(pageKey);
        return NavigateToView(pageType, parameter, clearNavigation);
    }

    public bool NavigateToViewModel(Type viewModelType, object? parameter = default, bool clearNavigation = false)
    {
        Type? pageType = _pageService.GetPageTypeByViewModel(viewModelType);
        return NavigateToView(pageType, parameter, clearNavigation);
    }

    public bool NavigateToViewModel(string viewModelKey, object? parameter = default, bool clearNavigation = false)
    {
        Type? pageType = _pageService.GetPageTypeByViewModel(viewModelKey);
        return NavigateToView(pageType, parameter, clearNavigation);
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is not Frame frame)
            return;

        bool clearNavigation = (bool)frame.Tag;

        if (clearNavigation)
            frame.BackStack.Clear();

        if (frame.GetPageViewModel() is INavigationAware navigationAware)
            navigationAware.OnNavigatedTo(e.Parameter);

        Navigated?.Invoke(sender, e);
    }
}
