using Codebreaker.ViewModels.Contracts.Services;

namespace Codebreaker.WPF.Views.Components;

/// <summary>
/// Interaction logic for InfoBarArea.xaml
/// </summary>
public partial class InfoBarArea : UserControl
{
    public InfoBarArea()
    {
        ViewModel = App.Current.GetService<IInfoBarService>();
        InitializeComponent();
        DataContext = this;
        ViewModel.New
            .WithTitle("Test")
            .WithMessage("TestMessage")
            .WithSeverity(ViewModels.Components.InfoMessageSeverity.Success)
            .Show();
    }

    public IInfoBarService ViewModel { get; }
}
