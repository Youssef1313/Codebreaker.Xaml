namespace Codebreaker.MAUI.Services.Navigation;

internal class PageServiceBuilder
{
    private readonly Dictionary<string, string> _pageRoutes = new();

    public PageServiceBuilder Configure(string key, string shellRoute)
    {
        _pageRoutes.Add(key, shellRoute);
        return this;
    }

    internal PageService Build() =>
        new PageService(_pageRoutes);
}