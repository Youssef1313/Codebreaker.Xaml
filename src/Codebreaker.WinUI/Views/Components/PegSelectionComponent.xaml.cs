using Codebreaker.ViewModels;

namespace CodeBreaker.WinUI.Views.Components;

internal sealed partial class PegSelectionComponent : UserControl
{
    public PegSelectionComponent()
    {
        InitializeComponent();
    }

    public GamePageViewModel ViewModel
    {
        get => (GamePageViewModel)GetValue(ViewModelProperty);
        set
        {
            SetValue(ViewModelProperty, value);
            DataContext = ViewModel;
        }
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(GamePageViewModel), typeof(PegSelectionComponent), new PropertyMetadata(null));

}
