using System.Collections;
using Microsoft.UI.Xaml.Data;

namespace CodeBreaker.WinUI.Converters;

internal class FirstOrDefaultConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object parameter, string language)
    {
        if (value is not IEnumerable enumerable)
            throw new ArgumentException("The value has to be an enumerable");

        foreach (var item in enumerable)
            return item;

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        throw new NotImplementedException();
}
