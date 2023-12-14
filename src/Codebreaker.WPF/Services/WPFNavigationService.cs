using Codebreaker.WPF.Contracts;
using Codebreaker.WPF.Views.Pages;

namespace Codebreaker.WPF.Services;

internal class WPFNavigationService : IWPFNavigationService
{
    private Frame? _frame;

    public Frame Frame
    {
        get
        {
            if (_frame is null)
                _frame = App.Current.MainWindow.Content as Frame ?? throw new InvalidOperationException("MainWindow content is not a frame");

            return _frame;
        }
        set
        {
            _frame = value;
        }
    }

    public bool CanGoBack { get; }

    public bool GoBack()
    {
        if (Frame.CanGoBack || _frame is null)
            return false;

        Frame.GoBack();
        return true;
    }

    public ValueTask<bool> GoBackAsync() =>
        ValueTask.FromResult(GoBack());

    public ValueTask<bool> NavigateToAsync(string key, object? parameter = null, bool clearNavigation = false)
    {
        return ValueTask.FromResult(Frame.Navigate(new GamePage())); // TODO Replace with page from pageService
    }
}
