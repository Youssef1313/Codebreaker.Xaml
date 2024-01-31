using Avalonia.Controls;
using Avalonia.Interactivity;
using CodeBreaker.Avalonia.Contracts.Services.Navigation;

namespace CodeBreaker.Avalonia;

public partial class Shell : UserControl
{
    public Shell()
    {
        DataContext = App.Current.GetService<IAvaloniaNavigationService>();
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        ((IAvaloniaNavigationService)DataContext!).Initialize();
        base.OnLoaded(e);
    }
}