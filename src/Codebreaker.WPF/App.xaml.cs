using Codebreaker.ViewModels.Contracts.Services;
using Codebreaker.WPF.Helpers;
using Codebreaker.WPF.Services;
using Codebreaker.WPF.Services.Navigation;
using Codebreaker.WPF.Views.Pages;
using Microsoft.Extensions.Configuration;

namespace Codebreaker.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application, IDisposable
{
    public static new App Current => (Application.Current as App) ?? throw new InvalidOperationException("The current application is no \"App\"");

    public static T GetService<T>()
        where T : class =>
        Current.DefaultScope.ServiceProvider.GetRequiredService<T>();

    private readonly IHost _host;

    public App()
    {
        this.SetDotnetEnvironmentVariable();
        var builder = Host.CreateApplicationBuilder();
        builder.Services.Configure<GamePageViewModelOptions>(options => { });
        builder.Services.AddNavigation<WPFNavigationService>(pages => pages
            .Configure<GamePage>("GamePage")
            .Configure<TestPage>("TestPage")
            .ConfigureInitialPage<GamePage>());
        builder.Services.AddTransient<IDialogService, WPFDialogService>();
        builder.Services.AddSingleton<IInfoBarService, InfoBarService>();
        builder.Services.AddScoped<GamePageViewModel>();
        builder.Services.AddHttpClient<IGamesClient, GamesClient>(client =>
        {
            client.BaseAddress = new Uri(builder.Configuration.GetRequired("ApiBase"));
        });
        _host = builder.Build();

        DefaultScope = _host.Services.CreateScope();
    }

    public IServiceScope DefaultScope { get; private init; }

    public void Dispose()
    {
        DefaultScope.Dispose();
        _host.Dispose();
    }
}
