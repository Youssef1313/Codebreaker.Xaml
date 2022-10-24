using CodeBreaker.ViewModels;
using Microsoft.UI.Xaml.Data;

namespace CodeBreaker.WinUI.Converters;

public class InfoBarSeverityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) =>
        value switch
        {
            InfoMessageSeverity.Warning => InfoBarSeverity.Warning,
            InfoMessageSeverity.Error => InfoBarSeverity.Error,
            InfoMessageSeverity.Success => InfoBarSeverity.Success,
            _ => InfoBarSeverity.Informational
        };

    public object ConvertBack(object value, Type targetType, object parameter, string language) =>
        value switch
        {
            InfoBarSeverity.Warning => InfoMessageSeverity.Warning,
            InfoBarSeverity.Error => InfoMessageSeverity.Error,
            InfoBarSeverity.Success => InfoMessageSeverity.Success,
            _ => InfoMessageSeverity.Info
        };
}
