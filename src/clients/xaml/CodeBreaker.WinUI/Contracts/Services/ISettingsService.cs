namespace CodeBreaker.WinUI.Contracts.Services;

public interface ISettingsService
{
    string LanguageKey { get; set; }
    ElementTheme Theme { get; set; }
    bool TrySettingStoredTheme();
}
