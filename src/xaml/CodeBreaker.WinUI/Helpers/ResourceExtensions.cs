using Microsoft.Windows.ApplicationModel.Resources;

namespace CodeBreaker.WinUI.Helpers;

internal static class ResourceExtensions
{
    private static readonly ResourceLoader s_resourceLoader = new();

    public static string GetLocalized(this string resourceKey) => s_resourceLoader.GetString(resourceKey);
}
