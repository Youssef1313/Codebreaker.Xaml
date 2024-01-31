using Codebreaker.ViewModels;

namespace CodeBreaker.WinUI.Views.Components;

public sealed partial class GameResultDisplay : UserControl
{
    public GameResultDisplay()
    {
        InitializeComponent();
    }

    public GamePageViewModel ViewModel
    {
        get => (GamePageViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(GamePageViewModel), typeof(GameResultDisplay), new PropertyMetadata(null));
}