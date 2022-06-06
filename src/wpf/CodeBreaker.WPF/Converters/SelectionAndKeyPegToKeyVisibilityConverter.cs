using CodeBreaker.ViewModels;

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CodeBreaker.WPF.Converters;

public class SelectionAndKeyPegToKeyVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(parameter);
        if (value is SelectionAndKeyPegs selection && int.TryParse(parameter.ToString(), out int ix))
        {
            return (ix < selection.KeyPegs.Length) 
                ? Visibility.Visible 
                : Visibility.Hidden;
        }
        else
        {
            return Visibility.Hidden;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
