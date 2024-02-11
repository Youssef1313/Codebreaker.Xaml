using Avalonia.Controls;
using Avalonia.Interactivity;
using Codebreaker.ViewModels;
using Codebreaker.ViewModels.Contracts.Services;
using CommunityToolkit.Mvvm.Messaging;

namespace CodeBreaker.Avalonia.Views.Pages;

public partial class GamePage : UserControl, IRecipient<GameMoveMessage>
{
    private readonly INavigationService _navigationService;

    public GamePage()
    {
        DataContext = App.Current.GetService<GamePageViewModel>();
        _navigationService = App.Current.GetService<INavigationService>();
        InitializeComponent();
        WeakReferenceMessenger.Default.Register(this);
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    public GamePageViewModel ViewModel => (GamePageViewModel)DataContext!;

    public void Receive(GameMoveMessage message)
    {
        if (message.GameMoveValue is not GameMoveValue.Completed)
            return;

        pegScrollViewer.UpdateLayout();
        pegScrollViewer.ScrollToEnd();
    }

    private void ToTestPageButtonClicked(object? sender, RoutedEventArgs e)
    {
        _navigationService.NavigateToAsync("TestPage");
    }
}