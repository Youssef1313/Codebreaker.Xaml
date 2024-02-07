using Codebreaker.ViewModels;
using CodeBreaker.Uno.Helpers;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;

namespace CodeBreaker.Uno.Views.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class GamePage : Page, IRecipient<GameMoveMessage>
{
    public GamePage()
    {
        ViewModel = App.Current.GetService<GamePageViewModel>();
        InitializeComponent();
    }

    public GamePageViewModel ViewModel { get; }

    public void Receive(GameMoveMessage message)
    {
        if (message.GameMoveValue is not GameMoveValue.Completed)
            return;

        var selectionAndKeyPegs = message.SelectionAndKeyPegs ?? throw new InvalidOperationException();
        var animationService = ConnectedAnimationService.GetForCurrentView();
        animationService.DefaultDuration = TimeSpan.FromMilliseconds(500);
        var container = listGameMoves.ItemContainerGenerator.ContainerFromItem(selectionAndKeyPegs);
        this.FindItemsOfType<Ellipse>(container)
            .ForEach((i, ellipse) =>
            {
                ConnectedAnimation? animation = animationService.GetAnimation($"guess{i}");

                // No animation found for this ellipxe -> the ellipse is most likely a key-peg
                if (animation is null)
                    return;

                animation.Configuration = new BasicConnectedAnimationConfiguration();
                animation.TryStart(ellipse);
            });

        // Scroll to bottom
        pegScrollViewer.UpdateLayout();
        pegScrollViewer.ScrollToVerticalOffset(pegScrollViewer.ScrollableHeight);
    }
}
