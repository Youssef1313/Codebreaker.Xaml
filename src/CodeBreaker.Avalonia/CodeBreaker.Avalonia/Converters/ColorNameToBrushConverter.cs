using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace CodeBreaker.Avalonia.Converters;

public class ColorNameToBrushConverter : IValueConverter
{
    private static readonly Brush s_orangeBrush = new SolidColorBrush(Colors.Orange);
    private static readonly Brush s_purpleBrush = new SolidColorBrush(Colors.Purple);
    private static readonly Brush s_redBrush = new SolidColorBrush(Color.FromRgb(209, 52, 56));
    private static readonly Brush s_greenBrush = new SolidColorBrush(Color.FromRgb(0, 173, 86));
    private static readonly Brush s_blueBrush = new SolidColorBrush(Color.FromRgb(79, 107, 237));
    private static readonly Brush s_yellowBrush = new SolidColorBrush(Color.FromRgb(252, 225, 0));
    private static readonly Brush s_emptyBrush = new SolidColorBrush(Color.FromRgb(160, 174, 178));

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            "Purple" => s_purpleBrush,
            "Orange" => s_orangeBrush,
            "Red" => s_redBrush,
            "Green" => s_greenBrush,
            "Blue" => s_blueBrush,
            "Yellow" => s_yellowBrush,
            _ => s_emptyBrush
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
