using CodeBreaker.ViewModels.Services;

namespace CodeBreaker.MAUI.Services;

public class MauiDialogService : IDialogService
{
    public Task ShowMessageAsync(string message)
    {
        return Task.CompletedTask;
    }
}
