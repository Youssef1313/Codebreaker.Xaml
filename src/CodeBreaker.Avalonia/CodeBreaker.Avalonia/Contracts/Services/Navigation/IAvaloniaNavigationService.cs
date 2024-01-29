using Avalonia.Controls;
using Codebreaker.ViewModels.Contracts.Services;

namespace CodeBreaker.Avalonia.Contracts.Services.Navigation;

internal interface IAvaloniaNavigationService : INavigationService
{
    ContentControl CurrentPage { get; }

    void Initialize();
}
