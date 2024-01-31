using Avalonia.Controls;

namespace CodeBreaker.Avalonia.Contracts.Services.Navigation;

internal interface IPageService
{
    string InitialPageKey { get; }

    UserControl GetInitialPage();

    UserControl GetPage(string key);

    public UserControl this[string key] => GetPage(key);
}