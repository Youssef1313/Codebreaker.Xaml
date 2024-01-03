using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Codebreaker.WPF.Views.Components;

/// <summary>
/// Interaction logic for GameResultDisplay.xaml
/// </summary>
[ObservableObject]
public partial class GameResultDisplay : UserControl
{
    public GameResultDisplay()
    {
        DataContext = this;
        InitializeComponent();
    }

    public GamePageViewModel ViewModel
    {
        get => (GamePageViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(GamePageViewModel), typeof(GameResultDisplay));


    //public GameMode GameMode
    //{
    //    get => (GameMode)GetValue(GameModeProperty);
    //    set
    //    {
    //        SetValue(GameModeProperty, value);
    //        Debug.WriteLine(value);
    //        OnPropertyChanged(nameof(GameMode));
    //    }
    //}

    //public static readonly DependencyProperty GameModeProperty =
    //    DependencyProperty.Register(nameof(GameMode), typeof(GameMode), typeof(GameResultDisplay), new PropertyMetadata(GameMode.NotRunning, OnGameModeChanged));

    //public ICommand? LostCommand
    //{
    //    get => (ICommand?)GetValue(LostCommandProperty);
    //    set => SetValue(LostCommandProperty, value);
    //}

    //public static readonly DependencyProperty LostCommandProperty =
    //    DependencyProperty.Register(nameof(LostCommand), typeof(ICommand), typeof(GameResultDisplay));

    //public Visibility LostButtonVisibility => LostCommand is not null ? Visibility.Visible : Visibility.Collapsed;

    //public ICommand? WonCommand
    //{
    //    get => (ICommand?)GetValue(WonCommandProperty);
    //    set => SetValue(WonCommandProperty, value);
    //}

    //public static readonly DependencyProperty WonCommandProperty =
    //    DependencyProperty.Register(nameof(WonCommand), typeof(ICommand), typeof(GameResultDisplay));

    //public Visibility WonButtonVisibility => WonCommand is not null ? Visibility.Visible : Visibility.Collapsed;

    //private static void OnGameModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //{
    //    var a = (GameResultDisplay)d; 
    //    a.OnPropertyChanged(nameof(GameMode));
    //}
}
