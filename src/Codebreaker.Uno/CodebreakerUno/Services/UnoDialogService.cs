using Codebreaker.ViewModels.Contracts.Services;
using Codebreaker.ViewModels.Services;

namespace CodebreakerUno.Services;

internal class UnoDialogService(IInfoBarService infoBarService) : IDialogService
{
    public Task ShowMessageAsync(string message)
    {
        infoBarService.New.WithMessage(message).Show();
        return Task.CompletedTask;
    }
}
