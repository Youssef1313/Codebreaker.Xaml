namespace Codebreaker.WPF.Views.Pages;

public partial class GamePage : Page
{
    public GamePage()
    {
        ViewModel = App.Current.GetService<GamePageViewModel>();

        DataContext = this;

        InitializeComponent();

        //WeakReferenceMessenger.Default.Register<GameStateChangedMessage>(this, (r, m) =>
        //{
        //    VisualStateManager.GoToElementState(MainGrid, m.GameMode.ToString(), true);
        //});
    }

    public GamePageViewModel ViewModel
    {
        get => (GamePageViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(GamePageViewModel), typeof(GamePage), new PropertyMetadata(null));
}
