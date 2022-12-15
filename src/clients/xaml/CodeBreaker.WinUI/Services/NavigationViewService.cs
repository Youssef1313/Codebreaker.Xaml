using CodeBreaker.WinUI.Contracts.Services;
using CodeBreaker.WinUI.CustomAttachedProperties;
using CodeBreaker.WinUI.Views.Pages;

namespace CodeBreaker.WinUI.Services;

public class NavigationViewService : INavigationViewService
{
    private readonly INavigationService _navigationService;

    private readonly IPageService _pageService;

    private NavigationView? _navigationView;

    public IList<object>? MenuItems => _navigationView?.MenuItems;

    public object? SettingsItem => _navigationView?.SettingsItem;

    public NavigationViewService(INavigationService navigationService, IPageService pageService)
    {
        _navigationService = navigationService;
        _pageService = pageService;
    }

    public void Initialize(NavigationView navigationView)
    {
        _navigationView = navigationView;
        _navigationView.BackRequested += OnBackRequested;
        _navigationView.ItemInvoked += OnItemInvoked;
    }

    public void UnregisterEvents()
    {
        if (_navigationView is not null)
        {
            _navigationView.BackRequested -= OnBackRequested;
            _navigationView.ItemInvoked -= OnItemInvoked;
        }
    }

    public NavigationViewItem? GetSelectedItem(Type pageType) => GetSelectedItem(_navigationView?.MenuItems, pageType);

    private void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => _navigationService.GoBack();

    private void OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        if (args.IsSettingsInvoked)
        {
            _navigationService.NavigateToView(typeof(SettingsPage));
        }
        else
        {
            if (args.InvokedItemContainer is not NavigationViewItem selectedItem)
                return;

            if (selectedItem.GetValue(NavigationViewItemHelper.CommandProperty) is ICommand command)
            {
                var commandArgument = selectedItem.GetValue(NavigationViewItemHelper.CommandArgumentProperty);
                command.Execute(commandArgument);
            }

            if (selectedItem.GetValue(NavigationHelper.NavigateByViewModelNameProperty) is string viewModelKey)
                _navigationService.NavigateToViewModel(viewModelKey);
            else if (selectedItem.GetValue(NavigationHelper.NavigateByViewModelTypeProperty) is Type viewModelType)
                _navigationService.NavigateToView(viewModelType);
            else if (selectedItem.GetValue(NavigationHelper.NavigateByPageNameProperty) is string pageKey)
                _navigationService.NavigateToView(pageKey);
            else if (selectedItem.GetValue(NavigationHelper.NavigateByPageTypeProperty) is Type pageType)
                _navigationService.NavigateToView(pageType);
        }
    }

    private NavigationViewItem? GetSelectedItem(IEnumerable<object>? menuItems, Type pageType)
    {
        if (menuItems is null)
            return null;

        foreach (var item in menuItems.OfType<NavigationViewItem>())
        {
            if (IsMenuItemForPageType(item, pageType))
                return item;

            NavigationViewItem? selectedChild = GetSelectedItem(item.MenuItems, pageType);

            if (selectedChild != null)
                return selectedChild;
        }

        return null;
    }

    private bool IsMenuItemForPageType(NavigationViewItem menuItem, Type sourcePageType)
    {
        if (menuItem.GetValue(NavigationHelper.NavigateByViewModelNameProperty) is string viewModelKey)
            return _pageService.GetPageTypeByViewModel(viewModelKey) == sourcePageType;

        if (menuItem.GetValue(NavigationHelper.NavigateByPageNameProperty) is string pageKey)
            return _pageService.GetPageTypeByPageName(pageKey) == sourcePageType;

        if (menuItem.GetValue(NavigationHelper.NavigateByViewModelTypeProperty) is Type viewModelType)
            return _pageService.GetPageTypeByViewModel(viewModelType) == sourcePageType;

        if (menuItem.GetValue(NavigationHelper.NavigateByPageTypeProperty) is Type pageType)
            return pageType == sourcePageType;

        return false;
    }
}
