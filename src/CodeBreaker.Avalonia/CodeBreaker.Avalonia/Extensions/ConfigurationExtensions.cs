using Avalonia.Platform;
using CodeBreaker.Avalonia;
using System;

namespace Microsoft.Extensions.Configuration;

internal static class ConfigurationExtensions
{
    public static void SetDotnetEnvironmentVariable(this App app)
    {
#if DEBUG
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");
#else
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Production");
#endif
    }

    public static string GetRequired(this IConfiguration configuration, string key) =>
        configuration[key] ?? throw new InvalidOperationException($"Configuration \"{key}\" was not found.");

    public static void AddAppSettingsJson(this ConfigurationManager configuration)
    {
        var assemblyName = typeof(App).Assembly.GetName().Name;
        configuration.AddJsonStream(AssetLoader.Open(new($"avares://{assemblyName}/appsettings.json")));
        configuration.AddJsonStream(AssetLoader.Open(new($"avares://{assemblyName}/appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json")));
    }
}
