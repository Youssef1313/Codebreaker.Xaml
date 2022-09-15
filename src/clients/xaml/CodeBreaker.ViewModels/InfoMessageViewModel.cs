using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeBreaker.ViewModels;

[ObservableObject]
public partial class InfoMessageViewModel
{
    public InfoMessageViewModel()
    {
        ActionCommand = new RelayCommand(() => IsVisible = false);
    }

    [ObservableProperty]
    private bool _isVisible = false;

    [ObservableProperty]
    private bool _isError = false;

    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private ICommand? _actionCommand;

    [ObservableProperty]
    private string? _actionTitle = "OK";

    public bool HasAction =>
        ActionCommand is not null && ActionTitle is not null;
}
