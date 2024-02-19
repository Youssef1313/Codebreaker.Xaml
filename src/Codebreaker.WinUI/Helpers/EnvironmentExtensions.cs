namespace Codebreaker.WinUI.Helpers;

internal static class EnvironmentExtensions
{
    public static void SetDotnetEnvironmentVariable(this Application app)
    {
#if DEBUG
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");
#else
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Production");
#endif
    }
}
