using CodeBreaker.ViewModels;

using System.Windows;
using System.Windows.Controls;

namespace CodeBreaker.WPF.Views;

public partial class PegSelectionView : UserControl
{
    public PegSelectionView()
    {
        DataContext = this;
        InitializeComponent();
    }

    public CodeBreaker6x4ViewModel ViewModel
    {
        get => (CodeBreaker6x4ViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(CodeBreaker6x4ViewModel), typeof(PegSelectionView), new PropertyMetadata(null));
}
