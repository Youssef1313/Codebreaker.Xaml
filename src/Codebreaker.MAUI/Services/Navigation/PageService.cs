using Codebreaker.MAUI.Contracts.Services.Navigation;

namespace Codebreaker.MAUI.Services.Navigation;

internal class PageService(Dictionary<string, string> pageRoutes) : IPageService
{
    public string GetPageRoute(string key)
    {
        string? shellRoute;

        lock (pageRoutes)
            if (!pageRoutes.TryGetValue(key, out shellRoute))
                throw new ArgumentException($"Page not found: {key}. Do you forget to configure the page?");

        return shellRoute;
    }
}
