using System.Windows.Input;

namespace Codebreaker.MAUI.Views.Components;

public partial class GameResultDisplay : ContentView
{
	public GameResultDisplay()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public GameMode GameMode
    {
        get => (GameMode)GetValue(GameModeProperty);
        set => SetValue(GameModeProperty, value);
    }

    public static readonly BindableProperty GameModeProperty =
        BindableProperty.Create(nameof(GameMode), typeof(GameMode), typeof(GameResultDisplay), GameMode.NotRunning);

    public ICommand? LostCommand
    {
        get => (ICommand?)GetValue(LostCommandProperty);
        set => SetValue(LostCommandProperty, value);
    }

    public static readonly BindableProperty LostCommandProperty =
        BindableProperty.Create(nameof(LostCommand), typeof(ICommand), typeof(GameResultDisplay), propertyChanged: OnLostCommandPropertyChanged);

    public bool IsLostButtonVisible => LostCommand is not null;

    public ICommand? WonCommand
    {
        get => (ICommand?)GetValue(WonCommandProperty);
        set => SetValue(WonCommandProperty, value);
    }

    public static readonly BindableProperty WonCommandProperty =
        BindableProperty.Create(nameof(LostCommand), typeof(ICommand), typeof(GameResultDisplay), propertyChanged: OnWonCommandPropertyChanged);

    public bool IsWonButtonVisible => WonCommand is not null;

    private static void OnWonCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var that = (GameResultDisplay)bindable;
        that.OnPropertyChanged(nameof(WonCommand));
        that.OnPropertyChanged(nameof(IsWonButtonVisible));
    }

    private static void OnLostCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var that = (GameResultDisplay)bindable;
        that.OnPropertyChanged(nameof(LostCommand));
        that.OnPropertyChanged(nameof(IsLostButtonVisible));
    }
}