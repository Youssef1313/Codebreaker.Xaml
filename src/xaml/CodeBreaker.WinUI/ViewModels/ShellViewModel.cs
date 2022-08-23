using CodeBreaker.Services;
using CodeBreaker.WinUI.Contracts.Services;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Navigation;

namespace CodeBreaker.WinUI.ViewModels;

[ObservableObject]
public partial class ShellViewModel
{
    private readonly IAuthService _authService;

    [ObservableProperty]
    private bool _isBackEnabled;

    [ObservableProperty]
    private object? _selected;

    public INavigationService NavigationService { get; }

    public INavigationViewService NavigationViewService { get; }

    //public bool IsBackEnabled
    //{
    //    get => _isBackEnabled;
    //    set => SetProperty(ref _isBackEnabled, value);
    //}

    //public object? Selected
    //{
    //    get => _selected;
    //    set => SetProperty(ref _selected, value);
    //}

    public bool IsAuthenticated => _authService.IsAuthenticatedAsync().Result;

    public bool IsNavigationPaneVisible { get; set; } = true;

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService, IAuthService authService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        _authService = authService;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;
        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }
}
