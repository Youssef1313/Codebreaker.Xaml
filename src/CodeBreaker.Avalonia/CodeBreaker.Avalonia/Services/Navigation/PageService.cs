using Avalonia.Controls;
using CodeBreaker.Avalonia.Contracts.Services.Navigation;
using System;
using System.Collections.Generic;

namespace CodeBreaker.Avalonia.Services.Navigation;

internal class PageService(Dictionary<string, Func<UserControl>> pages, string initialPageKey) : IPageService
{
    public string InitialPageKey =>
        initialPageKey;

    public UserControl GetInitialPage() =>
        GetPage(InitialPageKey);

    public UserControl GetPage(string key)
    {
        Func<UserControl>? pageFactory;

        lock (pages)
            if (!pages.TryGetValue(key, out pageFactory))
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");

        return pageFactory();
    }
}