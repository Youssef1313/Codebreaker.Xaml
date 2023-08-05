using CodeBreaker.ViewModels;

using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Media.Animation;

namespace CodeBreaker.WinUI.Views.Components;

public sealed partial class PegSelectionView : UserControl, IRecipient<GameMoveMessage>
{
    public PegSelectionView()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register(this);
        WeakReferenceMessenger.Default.UnregisterAllOnUnloaded(this);
    }

    public GamePageViewModel ViewModel
    {
        get => (GamePageViewModel)GetValue(ViewModelProperty);
        set
        {
            SetValue(ViewModelProperty, value);
            PegSelectionViewControl.DataContext = ViewModel;
        }
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(GamePageViewModel), typeof(PegSelectionView), new PropertyMetadata(null));

    public void Receive(GameMoveMessage message)
    {
        if (message.GameMoveValue is not GameMoveValue.Started)
            return;

        var connectedAnimation = ConnectedAnimationService.GetForCurrentView();
        var comboBoxes = this.FindItemsOfType<ComboBox>(this);
        ushort i = 0;
        foreach (var comboBox in comboBoxes)
        {
            connectedAnimation.PrepareToAnimate($"guess{i}", comboBox);
            i++;
        }
    }
}
