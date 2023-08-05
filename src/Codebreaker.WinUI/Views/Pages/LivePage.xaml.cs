using CodeBreaker.ViewModels;

using CommunityToolkit.Mvvm.Messaging;

using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;

namespace CodeBreaker.WinUI.Views.Pages;

public sealed partial class LivePage : Page
{
    public LivePageViewModel ViewModel { get; }

    public LivePage()
    {
        ViewModel = App.GetService<LivePageViewModel>();
        InitializeComponent();
    }
}
