using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using static CodeBreaker.Shared.Models.Data.Colors;

namespace CodeBreaker.WPF.Converters;

public class ColorNameToBrushConverter : IValueConverter
{
    private static readonly Brush s_blackBrush = new SolidColorBrush(Colors.Black);
    private static readonly Brush s_whiteBrush = new SolidColorBrush(Colors.White);
    private static Brush s_redBrush = new SolidColorBrush(Color.FromRgb(209, 52, 56));
    private static Brush s_greenBrush = new SolidColorBrush(Color.FromRgb(0, 173, 86));
    private static Brush s_blueBrush = new SolidColorBrush(Color.FromRgb(79, 107, 237));
    private static Brush s_yellowBrush = new SolidColorBrush(Color.FromRgb(252, 225, 0));
    private static Brush s_emptyBrush = new SolidColorBrush(Color.FromRgb(160, 174, 178));

    public Brush BlackBrush { get; set; } = s_blackBrush;
    public Brush WhiteBrush { get; set; } = s_whiteBrush;
    public Brush RedBrush { get; set; } = s_redBrush;
    public Brush GreenBrush { get; set; } = s_greenBrush;
    public Brush BlueBrush { get; set; } = s_blueBrush;
    public Brush YellowBrush { get; set; } = s_yellowBrush;
    private Brush EmptyBrush { get; set; } = s_emptyBrush;

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
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
