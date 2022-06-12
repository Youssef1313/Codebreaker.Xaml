using CodeBreaker.ViewModels.Services;

using Windows.UI.Popups;

namespace CodeBreaker.WinUI.Services;

internal class WinUIDialogService : IDialogService
{
    public async Task ShowMessageAsync(string message)
    {
        MessageDialog dlg = new(message);
        await dlg.ShowAsync();
    }
}
