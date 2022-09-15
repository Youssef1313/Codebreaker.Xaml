using CodeBreaker.ViewModels;

using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

using static CodeBreaker.Shared.Models.Data.Colors;

namespace CodeBreaker.WinUI.Converters;

public class SelectionAndKeyPegToKeyBrushConverter : IValueConverter
{
    private static readonly Brush s_blackBrush = new SolidColorBrush(Colors.Black);
    private static readonly Brush s_whiteBrush = new SolidColorBrush(Colors.White);
    private static readonly Brush s_emptyBrush = new SolidColorBrush(Colors.LightGray);

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();

        //ArgumentNullException.ThrowIfNull(parameter);
        //if (value is SelectionAndKeyPegs selection)
        //{
        //    if (int.TryParse(parameter.ToString(), out int ix))
        //    {
        //        if (selection.KeyPegs.White + selection.KeyPegs.Black <= (ix)) return s_emptyBrush;

        //        return selection.KeyPegs[ix] switch
        //        {
        //            Black => s_blackBrush,
        //            White => s_whiteBrush,
        //            _ => s_emptyBrush
        //        };
        //    }
        //    else
        //    {
        //        return s_emptyBrush;
        //    }
        //}
        //else
        //{
        //    throw new ArgumentException("value is not of type SelectionAndKeyPegs");
        //}
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
