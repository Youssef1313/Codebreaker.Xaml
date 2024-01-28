namespace Codebreaker.MAUI.Converters;

public class GameStatusToIsVisibleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not GameMode gameMode)
            throw new InvalidOperationException("GameStatusToVisibilityConverter received an incorrect value type");
        
        static bool GetStartVisibility(GameMode gameMode) => gameMode is GameMode.NotRunning;
        static bool GetRunningVisibility(GameMode gameMode) => gameMode is not GameMode.NotRunning;
        static bool GetCancelVisibility(GameMode gameMode) => gameMode is GameMode.Started or GameMode.MoveSet;
        static bool GetLostVisibility(GameMode gameMode) => gameMode is GameMode.Lost;
        static bool GetWonVisibility(GameMode gameMode) => gameMode is GameMode.Won;

        string uiCategory = parameter?.ToString() ?? throw new InvalidOperationException("Pass a parameter to this converter");

        return uiCategory switch
        {
            "Start" => GetStartVisibility(gameMode),
            "Running" => GetRunningVisibility(gameMode),
            "Cancelable" => GetCancelVisibility(gameMode),
            "Lost" => GetLostVisibility(gameMode),
            "Won" => GetWonVisibility(gameMode),
            _ => throw new InvalidOperationException("Invalid parameter value")
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
