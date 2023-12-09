using Codebreaker.ViewModels.Contracts.Services;
using Codebreaker.ViewModels.Services;

namespace CodeBreaker.WinUI.Services;

internal class WinUIDialogService(IInfoBarService infoBarService) : IDialogService
{
    public Task ShowMessageAsync(string message)
    {
        infoBarService.New.WithMessage(message).Show();
        return Task.CompletedTask;
    }
}
