using CodeBreaker.WinUI.Views.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CodeBreaker.WinUI.Contracts.Services.Navigation;
using Codebreaker.ViewModels.Services;

namespace CodeBreaker.WinUI.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    private IDialogService _dialogService;

    [ObservableProperty]
    private bool _isBackEnabled;

    [ObservableProperty]
    private object? _selected;

    public ShellViewModel(
        IWinUINavigationService navigationService,
        INavigationViewService navigationViewService,
        IDialogService dialogService
    )
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        _dialogService = dialogService;
    }

    public IWinUINavigationService NavigationService { get; }

    public INavigationViewService NavigationViewService { get; }

    public bool IsNavigationPaneVisible { get; set; } = true;

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
