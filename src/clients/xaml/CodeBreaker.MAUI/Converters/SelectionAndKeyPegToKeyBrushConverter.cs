using CodeBreaker.ViewModels;

using System.Globalization;

using static CodeBreaker.Shared.CodeBreakerColors;

namespace CodeBreaker.MAUI.Converters;

internal class SelectionAndKeyPegToKeyBrushConverter : IValueConverter
{
    private static Brush BlackBrush = new SolidColorBrush(Colors.Black);
    private static Brush WhiteBrush = new SolidColorBrush(Colors.White);
    private static Brush EmptyBrush = new SolidColorBrush(Colors.LightGray);

    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) return null;
        ArgumentNullException.ThrowIfNull(parameter);
        
        if (value is SelectionAndKeyPegs selection)
        {
            if (int.TryParse(parameter.ToString(), out int ix))
            {
                if (selection.KeyPegs.Length <= (ix)) return EmptyBrush;

                return selection.KeyPegs[ix] switch
                {
                    Black => BlackBrush,
                    White => WhiteBrush,
                    _ => EmptyBrush
                };
            }
            else
            {
                return EmptyBrush;
            }
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
