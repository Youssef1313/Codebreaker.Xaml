using CodeBreaker.WinUI.Contracts.Services.Navigation;
using CodeBreaker.WinUI.Services.Navigation;

namespace Microsoft.Extensions.DependencyInjection;

public static class PageServiceDIExtensions
{
    public static IServiceCollection AddPageService(this IServiceCollection services, Action<PageServiceBuilder> builder)
    {
        PageServiceBuilder pageServiceBuilder = new();
        builder(pageServiceBuilder);
        services.AddSingleton<IPageService>(pageServiceBuilder.Build());
        return services;
    }
}