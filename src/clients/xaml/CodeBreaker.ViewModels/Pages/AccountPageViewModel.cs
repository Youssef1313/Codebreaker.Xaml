﻿using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeBreaker.ViewModels.Pages;

public partial class AccountPageViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    private readonly IViewModelNavigationService _navigationService;

    public AccountPageViewModel(IAuthService authService, IViewModelNavigationService navigationService)
    {
        _authService = authService;
        _navigationService = navigationService;
    }

    public bool IsAuthenticated => _authService.IsAuthenticated;

    public UserInformation? UserInformation => _authService.LastUserInformation;

    [RelayCommand(AllowConcurrentExecutions = false, FlowExceptionsToTaskScheduler = true)]
    private async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        await _authService.LogoutAsync(cancellationToken);
        _navigationService.NavigateToViewModel(typeof(GamePageViewModel), clearNavigation: true);
    }
}
