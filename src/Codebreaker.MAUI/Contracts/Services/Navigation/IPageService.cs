namespace Codebreaker.MAUI.Contracts.Services.Navigation;

internal interface IPageService
{
    string GetPageRoute(string key);

    public string this[string key] => GetPageRoute(key);
}