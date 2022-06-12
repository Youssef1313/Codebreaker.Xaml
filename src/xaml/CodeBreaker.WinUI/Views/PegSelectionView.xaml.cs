using CodeBreaker.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CodeBreaker.WinUI.Views;

public sealed partial class PegSelectionView : UserControl
{
    public PegSelectionView()
    {
        this.InitializeComponent();
    }


    public CodeBreaker6x4ViewModel ViewModel
    {
        get => (CodeBreaker6x4ViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }


    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(CodeBreaker6x4ViewModel), typeof(PegSelectionView), new PropertyMetadata(null));


}
