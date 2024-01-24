using Codebreaker.ViewModels;

namespace CodeBreaker.Uno.Views.Components;

public sealed partial class GameResultDisplay : UserControl
{
    public GameResultDisplay()
    {
        InitializeComponent();
    }

    public GamePageViewModel ViewModel
    {
        get => (GamePageViewModel)GetValue(MyPropertyProperty); 
        set => SetValue(MyPropertyProperty, value);
    }

    public static readonly DependencyProperty MyPropertyProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(GamePageViewModel), typeof(GameResultDisplay), new PropertyMetadata(null));
}
