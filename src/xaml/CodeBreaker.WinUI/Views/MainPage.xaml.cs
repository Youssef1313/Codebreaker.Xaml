using CodeBreaker.ViewModels;
using CodeBreaker.WinUI.ViewModels;


namespace CodeBreaker.WinUI.Views;

public sealed partial class MainPage : Page
{
    public CodeBreaker6x4ViewModel ViewModel { get; }

    public MainPage()
    {
        ViewModel = App.GetService<CodeBreaker6x4ViewModel>();
        InitializeComponent();
    }

}
