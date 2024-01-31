using Avalonia.Controls;
using CodeBreaker.Avalonia.Contracts.Services.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeBreaker.Avalonia.Services.Navigation;

internal class AvaloniaNavigationService : ObservableObject, IAvaloniaNavigationService
{
    private readonly IPageService _pageService;

    private readonly Stack<string> _navigationStack = new (100);

    private ContentControl _currentPage = new UserControl();

    public AvaloniaNavigationService(IPageService pageService)
    {
        _pageService = pageService;
        _navigationStack.Push(pageService.InitialPageKey);
    }

    public ContentControl CurrentPage
    {
        get => _currentPage;
        private set
        {
            if (value is null || value == _currentPage)
                return;

            OnPropertyChanging(nameof(CurrentPage));
            _currentPage = value;
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    public bool CanGoBack => _navigationStack.Count != 0;

    public void Initialize()
    {
        CurrentPage = _pageService.GetInitialPage();
    }

    public ValueTask<bool> GoBackAsync()
    {
        if (_navigationStack.TryPop(out _) && _navigationStack.TryPeek(out var key))
            return NavigateToAsync(key);

        return ValueTask.FromResult(false);
    }

    public ValueTask<bool> NavigateToAsync(string key, object? parameter = null, bool clearNavigation = false)
    {
        UserControl page;

        try
        {
            page = _pageService[key];
        }
        catch (ArgumentException)
        {
            return ValueTask.FromResult(false);
        }

        CurrentPage = page;

        if (clearNavigation)
            _navigationStack.Clear();

        _navigationStack.Push(key);
        return ValueTask.FromResult(true);
    }
}
