global using CodeBreaker.WinUI.Helpers;

global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Controls;
global using Microsoft.UI.Xaml.Navigation;
global using Microsoft.Xaml.Interactivity;
using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.Services.Authentication.Definitions;
using CodeBreaker.ViewModels;
using CodeBreaker.ViewModels.Services;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

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
            services.Configure<GamePageViewModelOptions>(options => options.EnableDialogs = false);
            services.Configure<LiveClientOptions>(context.Configuration);

            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IViewModelNavigationService>(x => x.GetRequiredService<INavigationService>());
            services.AddScoped<IDialogService, WinUIDialogService>();
            services.AddSingleton<ISettingsService, SettingsService>();

            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();

            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddScoped<SettingsPageViewModel>();
            services.AddTransient<SettingsPage>();

            services.AddSingleton<IAuthService, AuthService>();
            services.AddScoped<AuthPageViewModel>();
            services.AddTransient<AuthPage>();

            services.AddHttpClient<IGameClient, GameClient>((HttpClient client) => client.BaseAddress = new(context.Configuration["ApiBase"]));
            services.AddScoped<GamePageViewModel>();
            services.AddTransient<GamePage>();

            services.AddSingleton<LiveClient>();
            services.AddScoped<LivePageViewModel>();
            services.AddTransient<LivePage>();
        })
        .Build();

    public static IServiceScope DefaultScope { get; } = s_host.Services.CreateScope();

    public static T GetService<T>()
        where T : class => 
        DefaultScope.ServiceProvider.GetRequiredService<T>();

    public static Window MainWindow { get; set; } = new Window() { Title = "AppDisplayName".GetLocalized() };

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
        IAuthService authService = GetService<IAuthService>();
        await authService.RegisterPersistentTokenCacheAsync();
        await authService.TryAquireTokenSilentlyAsync(new ApiServiceAuthDefinition());
        GetService<ISettingsService>().TrySettingStoredTheme();
        await GetService<IActivationService>().ActivateAsync(args);
    }
}
