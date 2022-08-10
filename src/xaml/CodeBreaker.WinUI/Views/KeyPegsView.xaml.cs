namespace CodeBreaker.WinUI.Views;

public sealed partial class KeyPegsView : UserControl
{
    public KeyPegsView()
    {
        InitializeComponent();
    }

    public IEnumerable<string> Pegs
    {
        get => (IEnumerable<string>)GetValue(PegsProperty);
        set => SetValue(PegsProperty, value);
    }

    public static readonly DependencyProperty PegsProperty =
        DependencyProperty.Register("Pegs", typeof(IEnumerable<string>), typeof(KeyPegsView), new PropertyMetadata(new[] {"Red"}));
}
