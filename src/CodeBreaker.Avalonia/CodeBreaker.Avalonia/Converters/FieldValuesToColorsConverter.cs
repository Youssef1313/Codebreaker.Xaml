using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CodeBreaker.Avalonia.Converters;

public class FieldValuesToColorsConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is IDictionary<string, string[]> data)
            return data["colors"];
        
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
