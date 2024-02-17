using Avalonia.Controls;
using Codebreaker.ViewModels.Contracts.Services;

namespace CodeBreaker.Avalonia.Views.Components;

public partial class InfoBarArea : UserControl
{
    public InfoBarArea()
    {
        InfoBarService = App.GetService<IInfoBarService>();
        InitializeComponent();
        DataContext = this;
    }

    public IInfoBarService InfoBarService { get; }
}