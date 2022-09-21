using CodeBreaker.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace CodeBreaker.WPF.Views.Pages;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        ViewModel = (Application.Current as App)?.GetService<GamePageViewModel>() ??
                throw new InvalidOperationException();

        DataContext = this;

        InitializeComponent();

        WeakReferenceMessenger.Default.Register<GameStateChangedMessage>(this, (r, m) =>
        {
            VisualStateManager.GoToElementState(MainGrid, m.GameMode.ToString(), true);
        });
    }

    public GamePageViewModel ViewModel
    {
        get => (GamePageViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(GamePageViewModel), typeof(MainWindow), new PropertyMetadata(null));
}
