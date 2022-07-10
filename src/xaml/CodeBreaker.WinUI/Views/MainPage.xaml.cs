using CodeBreaker.ViewModels;
using CodeBreaker.WinUI.ViewModels;

using CommunityToolkit.Mvvm.Messaging;

using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;

namespace CodeBreaker.WinUI.Views;

public sealed partial class MainPage : Page
{
    public CodeBreaker6x4ViewModel ViewModel { get; }

    public MainPage()
    {
        ViewModel = App.GetService<CodeBreaker6x4ViewModel>();
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<GameMoveMessage>(this, (r, m) =>
        {
            if (m.Value is GameMoveValue.Completed)
            {
                var selectionAndKeyPegs = m.SelectionAndKeyPegs;
                if (selectionAndKeyPegs is null) throw new InvalidOperationException();
                
                var connectedAnimation = ConnectedAnimationService.GetForCurrentView();
                var animation1 = connectedAnimation?.GetAnimation("guess1");
                var animation2 = connectedAnimation?.GetAnimation("guess2");
                var animation3 = connectedAnimation?.GetAnimation("guess3");
                var animation4 = connectedAnimation?.GetAnimation("guess4");

                var container = listGameMoves.ItemContainerGenerator.ContainerFromItem(selectionAndKeyPegs);

                var items = FindItems<Ellipse>(container);

                ConnectedAnimation?[] animations = new[] { animation1, animation2, animation3, animation4 };
                Ellipse[] targets = items.ToArray();
                
                for (int i = 0; i < 4; i++)
                {
                    animations[i]?.TryStart(targets[i]);
                }
            }
        });
    }

    private IEnumerable<T> FindItems<T>(DependencyObject obj)
        where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(obj, i);
            if (child is T item)
            {
                yield return item;
            }
            else
            {
                foreach (var childOfChild in FindItems<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
}
