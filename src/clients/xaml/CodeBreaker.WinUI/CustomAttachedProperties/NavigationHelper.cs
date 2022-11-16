namespace CodeBreaker.WinUI.CustomAttachedProperties;

// Helper class to set the navigation target for a NavigationViewItem.
//
// Usage in XAML:
// <NavigationViewItem x:Uid="Shell_Game" Icon="Document" helpers:Navigation.NavigateTo="AppName.ViewModels.MainViewModel" />
//
// Usage in code:
// Navigation.SetNavigateTo(navigationViewItem, typeof(MainViewModel).FullName);
public class NavigationHelper : DependencyObject
{
    #region ViewModelName
    public static readonly DependencyProperty NavigateByViewModelNameProperty =
        DependencyProperty.RegisterAttached(
            "NavigateByViewModelName",
            typeof(string),
            typeof(NavigationHelper),
            new PropertyMetadata(null)
        );
    public static string GetNavigateByViewModelName(NavigationViewItem item) =>
        (string)item.GetValue(NavigateByViewModelNameProperty);
    public static void SetNavigateByViewModelName(NavigationViewItem item, string value) =>
        item.SetValue(NavigateByViewModelNameProperty, value);
    #endregion

    #region ViewModelType
    public static readonly DependencyProperty NavigateByViewModelTypeProperty =
        DependencyProperty.RegisterAttached(
            "NavigateByViewModelType",
            typeof(Type),
            typeof(NavigationHelper),
            new PropertyMetadata(null)
        );
    public static string GetNavigateByViewModelType(NavigationViewItem item) =>
        (string)item.GetValue(NavigateByViewModelTypeProperty);
    public static void SetNavigateByViewModelType(NavigationViewItem item, Type value) =>
        item.SetValue(NavigateByViewModelTypeProperty, value);
    #endregion

    #region PageName
    public static readonly DependencyProperty NavigateByPageNameProperty =
        DependencyProperty.RegisterAttached(
            "NavigateByPageName",
            typeof(string),
            typeof(NavigationHelper),
            new PropertyMetadata(null)
        );
    public static string GetNavigateByPageName(NavigationViewItem item) =>
        (string)item.GetValue(NavigateByPageNameProperty);
    public static void SetNavigateByPageName(NavigationViewItem item, string value) =>
        item.SetValue(NavigateByPageNameProperty, value);
    #endregion

    #region PageType
    public static readonly DependencyProperty NavigateByPageTypeProperty =
        DependencyProperty.RegisterAttached(
            "NavigateByPageType",
            typeof(Type),
            typeof(NavigationHelper),
            new PropertyMetadata(null)
        );
    public static string GetNavigateByPageType(NavigationViewItem item) =>
        (string)item.GetValue(NavigateByPageTypeProperty);
    public static void SetNavigateByPageType(NavigationViewItem item, Type value) =>
        item.SetValue(NavigateByPageTypeProperty, value);
    #endregion
}
