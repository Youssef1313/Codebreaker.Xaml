using Microsoft.UI.Xaml.Data;

namespace CodeBreaker.WinUI.Converters;

internal class IsNotZeroToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        int number = value as int? ?? 0;
        return number != 0
            ? Visibility.Collapsed
            : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
