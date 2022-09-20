using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using static CodeBreaker.Shared.Models.Data.Colors;


namespace CodeBreaker.WinUI.Converters;

public class ColornameToBrushConverter : IValueConverter
{
    private static Brush s_blackBrush = new SolidColorBrush(Colors.Black);
    private static Brush s_whiteBrush = new SolidColorBrush(Colors.White);
    private static Brush s_redBrush = new SolidColorBrush(Color.FromArgb(255, 209, 52, 56));
    private static Brush s_greenBrush = new SolidColorBrush(Color.FromArgb(255, 0, 173, 86));
    private static Brush s_blueBrush = new SolidColorBrush(Color.FromArgb(255, 79, 107, 237));
    private static Brush s_yellowBrush = new SolidColorBrush(Color.FromArgb(255, 252, 225, 0));
    private static Brush s_emptyBrush = new SolidColorBrush(Color.FromArgb(255, 160, 174, 178));

    public Brush BlackBrush { get; set; } = s_blackBrush;
    public Brush WhiteBrush { get; set; } = s_whiteBrush;
    public Brush RedBrush { get; set; } = s_redBrush;
    public Brush GreenBrush { get; set; } = s_greenBrush;
    public Brush BlueBrush { get; set; } = s_blueBrush;
    public Brush YellowBrush { get; set; } = s_yellowBrush;
    private Brush EmptyBrush { get; set; } = s_emptyBrush;

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (value is not string guessPeg)
            throw new ArgumentException("Value is no string");

        return guessPeg switch
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

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
