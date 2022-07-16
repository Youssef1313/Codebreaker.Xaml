using CodeBreaker.ViewModels;

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using static CodeBreaker.Shared.CodeBreakerColors;

namespace CodeBreaker.WPF.Converters;

public class SelectionAndKeyPegToKeyBrushConverter : IValueConverter
{
    private static readonly Brush s_blackBrush = new SolidColorBrush(Colors.Black);
    private static readonly Brush s_whiteBrush = new SolidColorBrush(Colors.White);
    private static readonly Brush s_emptyBrush = new SolidColorBrush(Colors.LightGray);

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(parameter);
        if (value is SelectionAndKeyPegs selection)
        {
            if (int.TryParse(parameter.ToString(), out int ix))
            {
                if (selection.KeyPegs.Length <= (ix)) return s_emptyBrush;

                return selection.KeyPegs[ix] switch
                {
                    Black => s_blackBrush,
                    White => s_whiteBrush,
                    _ => s_emptyBrush
                };
            }
            else
            {
                return s_emptyBrush;
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
