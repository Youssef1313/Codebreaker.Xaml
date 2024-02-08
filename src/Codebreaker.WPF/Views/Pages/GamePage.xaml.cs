using Codebreaker.ViewModels.Contracts.Services;
using Codebreaker.WPF.Helpers;
using CommunityToolkit.Mvvm.Messaging;

namespace Codebreaker.WPF.Views.Pages;

public partial class GamePage : Page, IRecipient<GameMoveMessage>
{
    private readonly INavigationService _navigationService;

    public GamePage()
    {
        ViewModel = App.Current.GetService<GamePageViewModel>();
        _navigationService = App.Current.GetService<INavigationService>();
        DataContext = this;
        InitializeComponent();
        WeakReferenceMessenger.Default.Register(this);
        WeakReferenceMessenger.Default.UnregisterAllOnUnloaded(this);
    }

    public GamePageViewModel ViewModel
    {
        get => (GamePageViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(GamePageViewModel), typeof(GamePage), new PropertyMetadata(null));

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        await _navigationService.NavigateToAsync("TestPage");
    }

    public void Receive(GameMoveMessage message)
    {
        if (message.GameMoveValue is not GameMoveValue.Completed)
            return;

        pegScrollViewer.UpdateLayout();
        pegScrollViewer.ScrollToBottom();
    }
}
