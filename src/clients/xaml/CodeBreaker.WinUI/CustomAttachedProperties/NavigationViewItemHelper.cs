using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Microsoft.UI.Xaml.Media;

namespace CodeBreaker.WinUI.CustomAttachedProperties;

public class NavigationViewItemHelper : DependencyObject
{
    #region IconGlyph
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
    #endregion

    #region Command
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(NavigationViewItemHelper),
            new(new RelayCommand(() => { }))
        );
    public static void SetCommand(NavigationViewItem navigationViewItem, ICommand command) =>
        navigationViewItem.SetValue(CommandProperty, command);
    public static ICommand? GetCommand(NavigationViewItem navigationViewItem) =>
        (ICommand?)navigationViewItem.GetValue(CommandProperty);

    public static readonly DependencyProperty CommandArgumentProperty =
        DependencyProperty.Register(
            "CommandArgument",
            typeof(object),
            typeof(NavigationViewItemHelper),
            new(new RelayCommand(() => { }))
        );
    public static void SetCommandArgument(NavigationViewItem navigationViewItem, object? argument) =>
        navigationViewItem.SetValue(CommandArgumentProperty, argument);
    public static object? GetCommandArgument(NavigationViewItem navigationViewItem) =>
        (object?)navigationViewItem.GetValue(CommandArgumentProperty);
    #endregion
}
