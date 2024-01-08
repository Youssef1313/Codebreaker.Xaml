using CodebreakerUno.Helpers;
using CodebreakerUno.ViewModels;

namespace CodebreakerUno.Views.Pages;

public sealed partial class ShellPage : Page
{
    public ShellPage(ShellViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();

        App.Current.MainWindow!.ExtendsContentIntoTitleBar = true;
        AppTitleBarText.Text = "AppDisplayName".GetLocalized();
    }

    public ShellViewModel ViewModel { get; }
}
