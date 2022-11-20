using CodeBreaker.ViewModels.Services;
using CodeBreaker.WinUI.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeBreaker.WinUI.ViewModels;

[ObservableObject]
public partial class SettingsPageViewModel
{
    public record class LanguageSelectionItem(string Key, string Text);

    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    private bool _languageChangeInfoBarVisible = false;

    public SettingsPageViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        LanguageSelections = new LanguageSelectionItem[]
        {
            new (string.Empty, "Settings_Language_Default".GetLocalized()),
            new ("de", "Settings_Language_German".GetLocalized()),
            new ("en-US", "Settings_Language_English".GetLocalized())
        };
    }

    public LanguageSelectionItem[] LanguageSelections { get; private init; }

    public ElementTheme Theme
    {
        get => _settingsService.Theme;
        set
        {
            if (Theme == value)
                return;

            OnPropertyChanging(nameof(Theme));
            _settingsService.Theme = value;
            OnPropertyChanged(nameof(Theme));
        }
    }

    public string LanguageKey
    {
        get => _settingsService.LanguageKey;
        set
        {
            if (LanguageKey == value)
                return;

            OnPropertyChanging(nameof(LanguageKey));
            _settingsService.LanguageKey = value;
            OnPropertyChanged(nameof(LanguageKey));
            LanguageChangeInfoBarVisible = true;
        }
    }
}
