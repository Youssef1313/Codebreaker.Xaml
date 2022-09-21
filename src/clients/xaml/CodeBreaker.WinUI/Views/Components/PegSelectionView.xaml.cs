using CodeBreaker.ViewModels;

using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Media.Animation;

namespace CodeBreaker.WinUI.Views.Components;

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

        WeakReferenceMessenger.Default.UnregisterAllOnUnloaded(this);
    }

    public GamePageViewModel ViewModel
    {
        get => (GamePageViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(GamePageViewModel), typeof(PegSelectionView), new PropertyMetadata(null));
}
