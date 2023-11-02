using Codebreaker.ViewModels;

namespace CodeBreaker.WinUI.Views.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class GamePage : Page
{
    public GamePageViewModel ViewModel { get; }

    public GamePage()
    {
        ViewModel = App.GetService<GamePageViewModel>();
        InitializeComponent();
    }
}
