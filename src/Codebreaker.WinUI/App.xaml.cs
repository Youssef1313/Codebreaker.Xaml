global using CodeBreaker.WinUI.Helpers;

global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Controls;
global using Microsoft.UI.Xaml.Navigation;
global using Microsoft.Xaml.Interactivity;
using CodeBreaker.WinUI.Activation;
using CodeBreaker.WinUI.Contracts.Services;
using CodeBreaker.WinUI.Services;
using CodeBreaker.WinUI.ViewModels;
using CodeBreaker.WinUI.Views.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Windows.ApplicationModel;
using Xaml = Microsoft.UI.Xaml;
using WinUIEx;
using CodeBreaker.WinUI.Services.Navigation;
using CodeBreaker.WinUI.Contracts.Services.Navigation;
using Codebreaker.ViewModels.Contracts.Services;
using Codebreaker.ViewModels.Services;
using Codebreaker.GameAPIs.Client;
using Codebreaker.ViewModels;
using Codebreaker.WinUI.Helpers;

namespace CodeBreaker.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    public static new App Current => (Application.Current as App) ?? throw new InvalidOperationException("The current application is no \"App\"");

    public static T GetService<T>()
        where T : class => 
        Current.DefaultScope.ServiceProvider.GetRequiredService<T>();

    public static WindowEx MainWindow { get; } = new MainWindow();

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.SetDotnetEnvironmentVariable();
        var builder = Host.CreateApplicationBuilder();
        builder.Configuration.SetBasePath(Package.Current.InstalledLocation.Path);

        // Default Activation Handler
        builder.Services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

        // Services
        builder.Services.AddTransient<INavigationViewService, NavigationViewService>();

        builder.Services.AddSingleton<IActivationService, ActivationService>();
        builder.Services.AddPageService(builder => builder
            .Configure<GamePage>("GamePage")
            .Configure<SettingsPage>("SettingsPage")
            .Configure<ShellPage>("ShellPage"));
        builder.Services.AddSingleton<IWinUINavigationService, WinUINavigationService>();
        builder.Services.AddSingleton<INavigationService>(x => x.GetRequiredService<IWinUINavigationService>());
        builder.Services.AddScoped<IInfoBarService, InfoBarService>();
        builder.Services.AddScoped<IDialogService, WinUIDialogService>();
        builder.Services.AddSingleton<ISettingsService, SettingsService>();

        builder.Services.AddTransient<ShellPage>();
        builder.Services.AddTransient<ShellViewModel>();

        builder.Services.AddSingleton<ISettingsService, SettingsService>();
        builder.Services.AddScoped<SettingsPageViewModel>();
        builder.Services.AddTransient<SettingsPage>();

        builder.Services.AddHttpClient<IGamesClient, GamesClient>((HttpClient client) => client.BaseAddress = new(builder.Configuration.GetRequired("ApiBase")));
        builder.Services.AddScoped<GamePageViewModel>();
        var host = builder.Build();

        DefaultScope = host.Services.CreateScope();

        InitializeComponent();
        UnhandledException += App_UnhandledException;
    }

    public IServiceScope DefaultScope { get; private init; }

    private void App_UnhandledException(object sender, Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // For more details, see https://docs.microsoft.com/windows/winui/api/microsoft.ui.xaml.unhandledexceptioneventargs.
    }

    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used such as when the application is launched to open a specific file.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);
        GetService<ISettingsService>().TrySettingStoredTheme();
        await GetService<IActivationService>().ActivateAsync(args);
    }
}
