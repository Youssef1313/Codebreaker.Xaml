using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using static CodeBreaker.Shared.CodeBreakerColors;

namespace CodeBreaker.WPF.Converters;

public class PegColorToBrushConverter : IValueConverter
{
    private static Brush BlackBrush = new SolidColorBrush(Colors.Black);
    private static Brush WhiteBrush = new SolidColorBrush(Colors.White);
    private static Brush RedBrush = new SolidColorBrush(Colors.Red);
    private static Brush GreenBrush = new SolidColorBrush(Colors.Green);
    private static Brush BlueBrush = new SolidColorBrush(Colors.Blue);
    private static Brush YellowBrush = new SolidColorBrush(Colors.Yellow);
    private static Brush EmptyBrush = new SolidColorBrush(Colors.LightGray);

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            Black => BlackBrush,
            White => WhiteBrush,
            Red => RedBrush,
            Green => GreenBrush,
            Blue => BlueBrush,
            Yellow => YellowBrush,
            _ => EmptyBrush
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
