using Codebreaker.ViewModels.Contracts.Services;
using CodeBreaker.Avalonia.Contracts.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeBreaker.Avalonia.Services.Navigation;

internal static class NavigationServiceDIExtensions
{
    public static IServiceCollection AddNavigation<TNavigationService>(this IServiceCollection services, Action<PageServiceBuilder> builder)
        where TNavigationService : class, IAvaloniaNavigationService, INavigationService
    {
        PageServiceBuilder pageServiceBuilder = new();
        builder(pageServiceBuilder);
        services.AddSingleton<IPageService>(pageServiceBuilder.Build());
        services.AddSingleton<IAvaloniaNavigationService, TNavigationService>();
        services.AddSingleton<INavigationService>(x => x.GetRequiredService<IAvaloniaNavigationService>());
        return services;
    }
}
