using CodeBreaker.ViewModels;

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CodeBreaker.WPF.Converters;

public class SelectionAndKeyPegToKeyBrushConverter : IValueConverter
{
    private static Brush BlackBrush = new SolidColorBrush(Colors.Black);
    private static Brush WhiteBrush = new SolidColorBrush(Colors.White);
    private static Brush EmptyBrush = new SolidColorBrush(Colors.LightGray);

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(parameter);
        if (value is SelectionAndKeyPegs selection)
        {
            if (int.TryParse(parameter.ToString(), out int ix))
            {
                if (selection.KeyPegs.Length <= (ix)) return EmptyBrush;

                return selection.KeyPegs[ix] switch
                {
                    "black" => BlackBrush,
                    "white" => WhiteBrush,
                    _ => EmptyBrush
                };
            }
            else
            {
                return EmptyBrush;
            }
        }
        else
        {
            throw new ArgumentException("value is not of type SelectionAndKeyPegs");
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
