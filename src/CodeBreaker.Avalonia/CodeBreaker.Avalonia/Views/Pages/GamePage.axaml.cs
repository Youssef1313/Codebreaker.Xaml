using Avalonia.Controls;
using Codebreaker.ViewModels;

namespace CodeBreaker.Avalonia.Views.Pages;

public partial class GamePage : UserControl
{
    public GamePage()
    {
        DataContext = App.Current.GetService<GamePageViewModel>(); ;
        InitializeComponent();
    }

    public GamePageViewModel ViewModel => (GamePageViewModel)DataContext!;
}