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

    #endregion

    #region PageName

    #endregion

    #region PageType

    #endregion
}
