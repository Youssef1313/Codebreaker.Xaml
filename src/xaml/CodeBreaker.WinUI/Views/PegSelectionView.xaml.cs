using CodeBreaker.ViewModels;

using CommunityToolkit.Mvvm.Messaging;

using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CodeBreaker.WinUI.Views;

public sealed partial class PegSelectionView : UserControl
{
    public PegSelectionView()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<GameMoveMessage>(this, (r, m) =>
        {
            if (m.GameMoveValue is GameMoveValue.Started)
            {
                var connectedAnimation = ConnectedAnimationService.GetForCurrentView();
                connectedAnimation?.PrepareToAnimate("guess1", guess1);
                connectedAnimation?.PrepareToAnimate("guess2", guess2);
                connectedAnimation?.PrepareToAnimate("guess3", guess3);
                connectedAnimation?.PrepareToAnimate("guess4", guess4);
            }
        });
    }

    public CodeBreaker6x4ViewModel ViewModel
    {
        get => (CodeBreaker6x4ViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(CodeBreaker6x4ViewModel), typeof(PegSelectionView), new PropertyMetadata(null));
}
