namespace CodebreakerUno.Helpers;

internal static class ResourceExtensions
{
    private static readonly IStringLocalizer s_stringLocalizer = App.Current.GetService<IStringLocalizer>();

    public static string GetLocalized(this string resourceKey) => s_stringLocalizer.GetString(resourceKey);
}
