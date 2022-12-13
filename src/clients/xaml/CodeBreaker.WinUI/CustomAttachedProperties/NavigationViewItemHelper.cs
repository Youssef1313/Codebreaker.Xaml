using Microsoft.UI.Xaml.Media;

namespace CodeBreaker.WinUI.CustomAttachedProperties;

public class NavigationViewItemHelper : DependencyObject
{
    public static readonly DependencyProperty IconGlyphProperty =
        DependencyProperty.RegisterAttached(
            "IconGlyph",
            typeof(string),
            typeof(NavigationViewItemHelper),
            new(string.Empty)
        );

    public static void SetIconGlyph(NavigationViewItem navigationViewItem, string value)
    {
        navigationViewItem.Icon = new FontIcon()
        {
            FontFamily = navigationViewItem.Resources["SymbolThemeFontFamily"] as FontFamily,
            Glyph = value
        };
    }

    public static string GetIconGlyph(NavigationViewItem navigationViewItem)
    {
        if (navigationViewItem.Icon is not FontIcon fontIcon)
            return string.Empty;

        return fontIcon.Glyph ?? string.Empty;
    }
}
