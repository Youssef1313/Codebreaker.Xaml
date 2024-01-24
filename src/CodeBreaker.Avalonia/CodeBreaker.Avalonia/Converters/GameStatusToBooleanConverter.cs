using Avalonia.Data.Converters;
using Codebreaker.ViewModels;
using System;
using System.Globalization;

namespace CodeBreaker.Avalonia.Converters;

public class GameStatusToBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not GameMode gameMode)
            throw new InvalidOperationException($"{nameof(GameStatusToBooleanConverter)} received an incorrect value type");

        string uiCategory = parameter?.ToString() ?? throw new InvalidOperationException("Pass a parameter to this converter");

        var temp = uiCategory switch
        {
            "Start" => gameMode is GameMode.NotRunning,
            "Running" => gameMode is not GameMode.NotRunning,
            "Cancelable" => gameMode is GameMode.Started or GameMode.MoveSet,
            "Lost" => gameMode is GameMode.Lost,
            "Won" => gameMode is GameMode.Won,
            _ => throw new InvalidOperationException("Invalid parameter value")
        };

        return temp;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
