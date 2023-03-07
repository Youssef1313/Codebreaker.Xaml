using CodeBreaker.ViewModels;

using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;

namespace CodeBreaker.WinUI.Views.Pages;

public sealed partial class GamePage : Page, IRecipient<GameMoveMessage>
{
    public GamePageViewModel ViewModel { get; }

    public GamePage()
    {
        ViewModel = App.GetService<GamePageViewModel>();
        InitializeComponent();
        //this.ChangeNavigationPaneDisplayModeForThisPage(NavigationViewPaneDisplayMode.LeftCompact);   // Just for demonstration

        WeakReferenceMessenger.Default.Register(this);
        WeakReferenceMessenger.Default.UnregisterAllOnUnloaded(this);
    }

    public void Receive(GameMoveMessage message)
    {
        static void Animate(ConnectedAnimationService animationService, string key, UIElement target)
        {
            var animation = animationService.GetAnimation(key);

            if (animation is null)
                return;

            animation.Configuration = new BasicConnectedAnimationConfiguration();
            animation.TryStart(target);
        }

        if (message.GameMoveValue is GameMoveValue.Completed)
        {
            var selectionAndKeyPegs = message.SelectionAndKeyPegs;
            if (selectionAndKeyPegs is null) throw new InvalidOperationException();

            var animationService = ConnectedAnimationService.GetForCurrentView();
            animationService.DefaultDuration = TimeSpan.FromMilliseconds(500);
            var container = listGameMoves.ItemContainerGenerator.ContainerFromItem(selectionAndKeyPegs);

            var items = this.FindItemsOfType<Ellipse>(container).Take(4).ToArray(); // the first 4 ellipses are the guesses, the next 4 the key pegs

            for (int i = 0; i < 4; i++)
                Animate(animationService, $"guess{i}", items[i]);
        }
    }
}
