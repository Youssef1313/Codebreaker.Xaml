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
            if (args.InvokedItemContainer is NavigationViewItem selectedItem && 
                selectedItem.GetValue(NavigationHelper.NavigateToProperty) is string pageKey)
                _navigationService.NavigateToViewModel(pageKey);
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
        if (menuItem.GetValue(NavigationHelper.NavigateToProperty) is string pageKey)
            return _pageService.GetPageTypeByViewModel(pageKey) == sourcePageType;

        return false;
    }
}
