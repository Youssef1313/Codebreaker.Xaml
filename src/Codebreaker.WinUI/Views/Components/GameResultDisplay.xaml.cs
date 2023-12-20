using Codebreaker.ViewModels;

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
        DependencyProperty.Register("GameMode", typeof(GameMode), typeof(GameResultDisplay), new PropertyMetadata(GameMode.NotRunning));
}