using CodeBreaker.WinUI.Contracts.Services;
using CodeBreaker.WinUI.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using Windows.Foundation.Collections;
using Windows.Globalization;

namespace CodeBreaker.WinUI.Views.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsPage : Page
{
    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsPageViewModel>();
        InitializeComponent();
    }

    public SettingsPageViewModel ViewModel { get; }
}
