using Codebreaker.ViewModels.Contracts.Services;
using Codebreaker.ViewModels.Services;
using System.Threading.Tasks;

namespace CodeBreaker.Avalonia.Services;

internal class AvaloniaDialogService(IInfoBarService infoBarService) : IDialogService
{
    public Task ShowMessageAsync(string message)
    {
        infoBarService.New.WithMessage(message).Show();
        return Task.CompletedTask;
    }
}
