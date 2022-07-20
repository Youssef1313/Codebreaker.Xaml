using CodeBreaker.ViewModels;

using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Extensions.DependencyInjection;

using System.Windows;

namespace CodeBreaker.WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        ViewModel = (Application.Current as App)?.Services
            .GetRequiredService<CodeBreaker6x4ViewModel>() ??
                throw new InvalidOperationException();

        DataContext = this;

        InitializeComponent();

        WeakReferenceMessenger.Default.Register<GameStateChangedMessage>(this, (r, m) =>
        {
            VisualStateManager.GoToElementState(MainGrid, m.gameMode.ToString(), true);
        });
    }

    public CodeBreaker6x4ViewModel ViewModel
    {
        get => (CodeBreaker6x4ViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(CodeBreaker6x4ViewModel), typeof(MainWindow), new PropertyMetadata(null));
}
