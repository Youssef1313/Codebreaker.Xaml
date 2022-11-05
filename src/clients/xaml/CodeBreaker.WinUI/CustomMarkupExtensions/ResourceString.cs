using Microsoft.UI.Xaml.Markup;
using Windows.ApplicationModel.Resources;

namespace CodeBreaker.WinUI.CustomMarkupExtensions;

[MarkupExtensionReturnType(ReturnType = typeof(string))]
internal class ResourceString : MarkupExtension
{
    private static ResourceLoader resourceLoader = ResourceLoader.GetForViewIndependentUse();

    public string? Name { get; set; }

    protected override object ProvideValue()
    {
        if (string.IsNullOrEmpty(Name))
            return string.Empty;

        return resourceLoader.GetString(Name);
    }
}
