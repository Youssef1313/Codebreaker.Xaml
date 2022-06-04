using CodeBreaker.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System.Windows;

namespace CodeBreaker.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = (Application.Current as App)?.Services.GetRequiredService<CodeBreaker6x4ViewModel>() ?? throw new InvalidOperationException();
    }

    public CodeBreaker6x4ViewModel ViewModel { get; }
}
