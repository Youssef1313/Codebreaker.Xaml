namespace Codebreaker.WPF.Views.Components;

/// <summary>
/// Interaction logic for GameResultDisplay.xaml
/// </summary>
public partial class GameResultDisplay : UserControl
{
    public GameResultDisplay()
    {
        InitializeComponent();
    }

    public GamePageViewModel ViewModel
    {
        get { return (GamePageViewModel)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(GamePageViewModel), typeof(GameResultDisplay));
}
