using Avalonia.Controls;
using Avalonia.Interactivity;
using Codebreaker.ViewModels;
using Codebreaker.ViewModels.Contracts.Services;

namespace CodeBreaker.Avalonia.Views.Pages;

public partial class GamePage : UserControl
{
    private readonly INavigationService _navigationService;

    public GamePage()
    {
        DataContext = App.Current.GetService<GamePageViewModel>();
        InitializeComponent();
        _navigationService = App.Current.GetService<INavigationService>();
    }

    public GamePageViewModel ViewModel => (GamePageViewModel)DataContext!;

    private void ToTestPageButtonClicked(object? sender, RoutedEventArgs e)
    {
        _navigationService.NavigateToAsync("TestPage");
    }
}