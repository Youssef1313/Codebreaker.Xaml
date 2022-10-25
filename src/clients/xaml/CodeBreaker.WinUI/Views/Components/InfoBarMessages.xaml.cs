using System.Collections.ObjectModel;
using CodeBreaker.ViewModels;

namespace CodeBreaker.WinUI.Views.Components;

public sealed partial class InfoBarMessages : UserControl
{
    public InfoBarMessages()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty MessagesProperty = DependencyProperty.Register(
        "Messages",
        typeof(ObservableCollection<InfoMessageViewModel>),
        typeof(InfoBarMessages),
        new (new ObservableCollection<InfoMessageViewModel>())
    );

    public ObservableCollection<InfoMessageViewModel> Messages
    {
        get => (ObservableCollection<InfoMessageViewModel>)GetValue(MessagesProperty);
        set => SetValue(MessagesProperty, value);
    }
}
