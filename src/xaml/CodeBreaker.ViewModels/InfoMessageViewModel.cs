using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeBreaker.ViewModels;

[ObservableObject]
public partial class InfoMessageViewModel
{
    [ObservableProperty]
    private bool _isVisible = false;

    [ObservableProperty]
    private bool _isError = false;

    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private string _title = string.Empty;
}
