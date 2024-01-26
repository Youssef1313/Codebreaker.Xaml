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
        configuration.AddJsonFile("appsettings.json", true);
        configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json");
    }
}
