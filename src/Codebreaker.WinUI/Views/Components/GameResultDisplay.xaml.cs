using Codebreaker.ViewModels;
using System.Windows.Input;

namespace CodeBreaker.WinUI.Views.Components;

public sealed partial class GameResultDisplay : UserControl
{
    public GameResultDisplay()
    {
        InitializeComponent();
    }

    public GameMode GameMode
    {
        get => (GameMode)GetValue(GameModeProperty);
        set => SetValue(GameModeProperty, value);
    }

    public static readonly DependencyProperty GameModeProperty =
        DependencyProperty.Register(nameof(GameMode), typeof(GameMode), typeof(GameResultDisplay), new PropertyMetadata(GameMode.NotRunning));

    public ICommand? LostCommand
    {
        get => (ICommand?)GetValue(LostCommandProperty);
        set => SetValue(LostCommandProperty, value);
    }

    public static readonly DependencyProperty LostCommandProperty =
        DependencyProperty.Register(nameof(LostCommand), typeof(ICommand), typeof(GameResultDisplay), new PropertyMetadata(null));

    public Visibility LostButtonVisibility => LostCommand is not null ? Visibility.Visible : Visibility.Collapsed;

    public ICommand? WonCommand
    {
        get => (ICommand?)GetValue(WonCommandProperty);
        set => SetValue(WonCommandProperty, value);
    }

    public static readonly DependencyProperty WonCommandProperty =
        DependencyProperty.Register(nameof(WonCommand), typeof(ICommand), typeof(GameResultDisplay), new PropertyMetadata(null));

    public Visibility WonButtonVisibility => WonCommand is not null ? Visibility.Visible : Visibility.Collapsed;
}