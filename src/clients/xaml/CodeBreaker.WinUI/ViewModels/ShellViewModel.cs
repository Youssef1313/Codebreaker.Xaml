using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.Services.Authentication.Definitions;
using CodeBreaker.ViewModels.Services;
using CodeBreaker.ViewModels;
using CodeBreaker.WinUI.Contracts.Services;
using CodeBreaker.WinUI.Services;
using CodeBreaker.WinUI.Views.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeBreaker.WinUI.ViewModels;

[ObservableObject]
public partial class ShellViewModel
{
    private IAuthService _authService;

    private static readonly IAuthDefinition _authDefinition = new ApiServiceAuthDefinition();

    private IDialogService _dialogService;

    [ObservableProperty]
    private bool _isBackEnabled;

    [ObservableProperty]
    private object? _selected;

    public ShellViewModel(
        INavigationService navigationService,
        INavigationViewService navigationViewService,
        IAuthService authService,
        IDialogService dialogService
    )
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        _authService = authService;
        _dialogService = dialogService;
        _authService.OnAuthenticationStateChanged += (sender, args) =>
        {
            OnPropertyChanged(nameof(IsAuthenticated));
            OnPropertyChanged(nameof(User));
        };
    }

    public INavigationService NavigationService { get; }

    public INavigationViewService NavigationViewService { get; }

    public bool IsNavigationPaneVisible { get; set; } = true;

    public UserInformation? User => _authService.LastUserInformation;

    public bool IsAuthenticated => _authService.IsAuthenticated;

    [RelayCommand(AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task LoginAsync(CancellationToken cancellation = default)
    {
        try
        {
            await _authService.AquireTokenAsync(_authDefinition, cancellation);
        }
        catch (Exception)
        {
            await _dialogService.ShowMessageAsync("Could not authenticate");
            return;
        }
    }

    [RelayCommand(AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        return _authService.LogoutAsync(cancellationToken);
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }
}
