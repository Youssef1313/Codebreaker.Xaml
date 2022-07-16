using CodeBreaker.ViewModels;

using Microsoft.UI.Xaml.Data;

namespace CodeBreaker.WinUI.Converters;

public class SelectionAndKeyPegToKeyVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        ArgumentNullException.ThrowIfNull(parameter);
        
        if (value is SelectionAndKeyPegs selection && int.TryParse(parameter.ToString(), out int ix))
        {
            return (ix < selection.KeyPegs.Length) 
                ? Visibility.Visible 
                : Visibility.Collapsed;
        }
        else
        {
            return Visibility.Collapsed;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
