using CodeBreaker.ViewModels;
using CodeBreaker.WinUI.Helpers;

using CommunityToolkit.Mvvm.Messaging;

using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;

namespace CodeBreaker.WinUI.Views;

public sealed partial class GamePage : Page
{
    public GamePageViewModel ViewModel { get; }

    public GamePage()
    {
        ViewModel = App.GetService<GamePageViewModel>();
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<GameMoveMessage>(this, (r, m) =>
        {
            static void Animate(ConnectedAnimationService animationService, string key, UIElement target)
            {     
                var animation = animationService?.GetAnimation(key);
                // the default animation configuration is a GravityConnectedAnimationConfiguration, which is good for this scenario
                
                if (animation is null) return;
                
                animation.TryStart(target);
            }
            
            if (m.GameMoveValue is GameMoveValue.Completed)
            {
                var selectionAndKeyPegs = m.SelectionAndKeyPegs;
                if (selectionAndKeyPegs is null) throw new InvalidOperationException();
                
                var animationService = ConnectedAnimationService.GetForCurrentView();
                var container = listGameMoves.ItemContainerGenerator.ContainerFromItem(selectionAndKeyPegs);
       
                var items = FindItemsOfType<Ellipse>(container).Take(4).ToArray(); // the first 4 ellipses are the guesses, the next 4 the key pegs
                for (int i = 0; i < 4; i++)
                {
                    Animate(animationService, $"guess{i + 1}", items[i]);
                }
            }
        });

        WeakReferenceMessenger.Default.UnregisterAllOnUnloaded(this);
    }

    private IEnumerable<T> FindItemsOfType<T>(DependencyObject obj)
        where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(obj, i);

            if (child is null)
                yield break;

            if (child is T item)
                yield return item;

            foreach (T childOfChild in FindItemsOfType<T>(child))
                yield return childOfChild;
        }
    }
}
