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

namespace CodeBreaker.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    private static readonly IHost s_host = Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(options =>
        {
            options.SetBasePath(Package.Current.InstalledLocation.Path);
        })
        .ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers

            // Services

            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddPageService(builder => builder
                .Configure<SettingsPage>("GamePage")
                //.Configure<SettingsPage>("SettingsPage")
                .Configure<ShellPage>("ShellPage"));
            services.AddSingleton<IWinUINavigationService, WinUINavigationService>();
            services.AddSingleton<INavigationService>(x => x.GetRequiredService<IWinUINavigationService>());
            services.AddScoped<IDialogService, WinUIDialogService>();
            services.AddSingleton<ISettingsService, SettingsService>();

            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();

            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddScoped<SettingsPageViewModel>();
            services.AddTransient<SettingsPage>();

            string apiBase = context.Configuration["ApiBase"] ?? throw new InvalidOperationException("ApiBase configuration not found");
            services.AddHttpClient<IGamesClient, GamesClient>((HttpClient client) => client.BaseAddress = new(apiBase));
            services.AddScoped<GamePageViewModel>();
            //services.AddTransient<GamePage>();
        })
        .Build();

    public static IServiceScope DefaultScope { get; } = s_host.Services.CreateScope();

    public static T GetService<T>()
        where T : class => 
        DefaultScope.ServiceProvider.GetRequiredService<T>();

    public static WindowEx MainWindow { get; } = new MainWindow();

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
#if DEBUG
        // Settings the environment variable here, because environment variables in launchsettings.json get ignored
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");
#else
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Production");
#endif

        InitializeComponent();
        UnhandledException += App_UnhandledException;
    }

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
