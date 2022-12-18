using CodeBreaker.ViewModels.Pages;

namespace CodeBreaker.WinUI.Views.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class AccountPage : Page
{
    public AccountPage()
    {
        ViewModel = App.GetService<AccountPageViewModel>();
        InitializeComponent();
    }

    public AccountPageViewModel ViewModel { get; private init; }
}
