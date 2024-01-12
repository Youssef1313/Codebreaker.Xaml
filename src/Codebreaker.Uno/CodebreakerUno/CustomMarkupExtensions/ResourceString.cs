using CodebreakerUno.Helpers;
using Microsoft.UI.Xaml.Markup;

namespace CodebreakerUno.CustomMarkupExtensions;

[MarkupExtensionReturnType(ReturnType = typeof(string))]
internal class ResourceString : MarkupExtension
{
    public string? Name { get; set; }

    protected override object ProvideValue() =>
        string.IsNullOrEmpty(Name)
        ? string.Empty
        : Name.GetLocalized();
}
