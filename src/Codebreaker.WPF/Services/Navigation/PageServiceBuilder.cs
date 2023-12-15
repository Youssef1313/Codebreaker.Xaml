namespace Codebreaker.WPF.Services.Navigation;

internal class PageServiceBuilder
{
    private readonly Dictionary<string, Func<Page>> _pages = new();

    private Func<Page>? _initialPage;

    public PageServiceBuilder Configure<V>()
        where V : Page, new() =>
        Configure<V>(typeof(V).Name);

    public PageServiceBuilder Configure<V>(string key)
        where V : Page, new()
    {
        lock (_pages)
        {
            if (_pages.ContainsKey(key))
                throw new ArgumentException($"The key {key} is already configured in {nameof(PageService)}");

            var pageFactory = () => new V();

            if (_pages.ContainsValue(pageFactory))
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == pageFactory).Key}");

            _pages.Add(key, pageFactory);
        }

        return this;
    }

    public PageServiceBuilder ConfigureInitialPage<V>()
        where V : Page, new()
    {
        _initialPage = () => new V();
        return this;
    }

    public PageService Build()
    {
        if (_initialPage is null)
            throw new ArgumentNullException(nameof(_initialPage), $"The initial page must be configured. Did you call .{nameof(ConfigureInitialPage)}?");

        return new(_pages, _initialPage);
    }
}