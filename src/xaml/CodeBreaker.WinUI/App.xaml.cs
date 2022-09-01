global using CodeBreaker.WinUI.Helpers;

global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Controls;
global using Microsoft.UI.Xaml.Navigation;
global using Microsoft.Xaml.Interactivity;

using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.ViewModels;
using CodeBreaker.ViewModels.Services;
using CodeBreaker.WinUI.Activation;
using CodeBreaker.WinUI.Contracts.Services;
using CodeBreaker.WinUI.Services;
using CodeBreaker.WinUI.ViewModels;
using CodeBreaker.WinUI.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        .ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers

            // Services
            services.Configure<CodeBreaker6x4ViewModelOptions>(options => options.EnableDialogs = false);

            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<INavigationServiceCore>(x => x.GetRequiredService<INavigationService>());
            services.AddScoped<IDialogService, WinUIDialogService>();

            services.AddSingleton(x => new HubConnectionBuilder()
                .WithUrl("https://codebreakerlive.purplebush-9a246700.westeurope.azurecontainerapps.io/live")
                //.WithUrl("http://localhost:5131/live")
                .WithAutomaticReconnect()
                .ConfigureLogging(x =>
                {
                    x.SetMinimumLevel(LogLevel.Information);
                    x.AddDebug();
                    x.AddConsole();
                })
                .Build());

            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<AuthPageViewModel>();
            services.AddTransient<AuthPage>();

            services.AddHttpClient<IGameClient, GameClient>((serviceProvider, client) =>
            {
                client.BaseAddress = new("https://codebreakerapi.purplebush-9a246700.westeurope.azurecontainerapps.io");
                //client.BaseAddress = new("http://localhost:9400");
            });
            services.AddScoped<CodeBreaker6x4ViewModel>();
            services.AddTransient<GamePage>();

            services.AddSingleton<LiveClient>();
            services.AddScoped<LivePageViewModel>();
            services.AddTransient<LivePage>();
        })
        .Build();

    public static T GetService<T>()
        where T : class => 
        s_host.Services.GetRequiredService<T>();

    public static Window MainWindow { get; set; } = new Window() { Title = "AppDisplayName".GetLocalized() };

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
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
        var activationService = GetService<IActivationService>();
        await activationService.ActivateAsync(args);
    }
}
