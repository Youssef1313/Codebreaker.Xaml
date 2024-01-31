using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBreaker.Avalonia.Services.Navigation;

internal class PageServiceBuilder
{
    private readonly Dictionary<string, Func<UserControl>> _pages = new();

    private string? _initialPage;

    public PageServiceBuilder Configure<TView>()
        where TView : UserControl, new() =>
        Configure<TView>(typeof(TView).Name);

    public PageServiceBuilder Configure<TView>(string key)
        where TView : UserControl, new()
    {
        lock (_pages)
        {
            if (_pages.ContainsKey(key))
                throw new ArgumentException($"The key {key} is already configured in {nameof(PageService)}");

            var pageFactory = () => new TView();

            if (_pages.ContainsValue(pageFactory))
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == pageFactory).Key}");

            _pages.Add(key, pageFactory);
        }

        return this;
    }

    public PageServiceBuilder ConfigureInitialPage(string key)
    {
        _initialPage = key;
        return this;
    }

    public PageService Build()
    {
        if (_initialPage is null)
            throw new ArgumentNullException(nameof(_initialPage), $"The initial page must be configured. Did you call .{nameof(ConfigureInitialPage)}?");

        return new(_pages, _initialPage);
    }
}