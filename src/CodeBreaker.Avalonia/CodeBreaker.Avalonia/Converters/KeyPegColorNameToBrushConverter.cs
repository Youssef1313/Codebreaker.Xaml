using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace CodeBreaker.Avalonia.Converters;

public class KeyPegColorNameToBrushConverter : IValueConverter
{
    private static readonly Brush s_blackBrush = new SolidColorBrush(Colors.Black);
    private static readonly Brush s_whiteBrush = new SolidColorBrush(Colors.White);
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            "Black" => s_blackBrush,
            "White" => s_whiteBrush,
            _ => value
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
