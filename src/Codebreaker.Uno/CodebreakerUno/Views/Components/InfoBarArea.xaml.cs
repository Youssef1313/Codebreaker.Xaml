using Codebreaker.ViewModels.Contracts.Services;

namespace CodebreakerUno.Views.Components;

internal sealed partial class InfoBarArea : UserControl
{
    public InfoBarArea()
    {
        ViewModel = App.Current.GetService<IInfoBarService>();
        InitializeComponent();
    }

    public IInfoBarService ViewModel { get; }
}
