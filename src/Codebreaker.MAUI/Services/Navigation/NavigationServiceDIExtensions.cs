using Codebreaker.MAUI.Contracts.Services.Navigation;
using Codebreaker.MAUI.Services.Navigation;
using Codebreaker.ViewModels.Contracts.Services;

namespace Microsoft.Extensions.DependencyInjection;

internal static class NavigationServiceDIExtensions
{
    public static IServiceCollection AddNavigation<TNavigationService>(this IServiceCollection services, Action<PageServiceBuilder> builder)
        where TNavigationService : class, INavigationService
    {
        PageServiceBuilder pageServiceBuilder = new();
        builder(pageServiceBuilder);
        services.AddScoped<IPageService>(_ => pageServiceBuilder.Build());
        services.AddScoped<INavigationService, TNavigationService>();
        return services;
    }
}