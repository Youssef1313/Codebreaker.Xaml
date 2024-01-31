using Avalonia.Data.Converters;
using Codebreaker.ViewModels.Components;
using System;
using System.Globalization;

namespace CodeBreaker.Avalonia.Converters;

public class InfoBarSeverityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo cultureInfo)
    {
        if (value is not InfoMessageSeverity severity)
            throw new ArgumentException($"Expected value to be {nameof(InfoMessageSeverity)}");

        if (parameter is not string expectedSeverity)
            throw new ArgumentException("Expected parameter to be a string");

        var test = severity.ToString();
        return severity.ToString() == expectedSeverity;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo cultureInfo) =>
        throw new NotImplementedException();
}
