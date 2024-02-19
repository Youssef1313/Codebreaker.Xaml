namespace Codebreaker.WPF.Helpers;

internal static class EnvironmentExtensions
{
    public static void SetDotnetEnvironmentVariable(this App app)
    {
#if DEBUG
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");
#else
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Production");
#endif
    }
}
