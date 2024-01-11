using Codebreaker.ViewModels.Contracts.Services;
using Codebreaker.ViewModels.Services;
using CodebreakerUno.Contracts.Services.Navigation;
using CodebreakerUno.Services.Navigation;
using CodebreakerUno.ViewModels;
using CodebreakerUno.Views.Pages;

namespace CodebreakerUno;

public class App : Application
{
    protected internal Window? MainWindow { get; private set; }

    protected IHost? Host { get; private set; }

    internal new static App Current => (App)Application.Current;

    public IServiceScope? DefaultScope { get; private set; }

    public T GetService<T>()
        where T : class =>
        DefaultScope!.ServiceProvider.GetRequiredService<T>();

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        var builder = this.CreateBuilder(args)
            .Configure(host => host
#if DEBUG
                // Switch to Development environment when running in DEBUG
                .UseEnvironment(Environments.Development)
#endif
                .UseLogging(configure: (context, logBuilder) =>
                {
                    // Configure log levels for different categories of logging
                    logBuilder
                        .SetMinimumLevel(
                            context.HostingEnvironment.IsDevelopment() ?
                                LogLevel.Information :
                                LogLevel.Warning)

                        // Default filters for core Uno Platform namespaces
                        .CoreLogLevel(LogLevel.Warning);

                    // Uno Platform namespace filter groups
                    // Uncomment individual methods to see more detailed logging
                    //// Generic Xaml events
                    //logBuilder.XamlLogLevel(LogLevel.Debug);
                    //// Layout specific messages
                    //logBuilder.XamlLayoutLogLevel(LogLevel.Debug);
                    //// Storage messages
                    //logBuilder.StorageLogLevel(LogLevel.Debug);
                    //// Binding related messages
                    //logBuilder.XamlBindingLogLevel(LogLevel.Debug);
                    //// Binder memory references tracking
                    //logBuilder.BinderMemoryReferenceLogLevel(LogLevel.Debug);
                    //// DevServer and HotReload related
                    //logBuilder.HotReloadCoreLogLevel(LogLevel.Information);
                    //// Debug JS interop
                    //logBuilder.WebAssemblyLogLevel(LogLevel.Debug);

                }, enableUnoLogging: true)
                .UseConfiguration(configure: configBuilder =>
                    configBuilder
                        .EmbeddedSource<App>()
                        .Section<AppConfig>()
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                // Register Json serializers (ISerializer and ISerializer)
                .UseSerialization((context, services) => services
                    .AddContentSerializer(context))
                .UseHttp((context, services) => services
                    // Register HttpClient
#if DEBUG
                    // DelegatingHandler will be automatically injected into Refit Client
                    .AddTransient<DelegatingHandler, DebugHttpHandler>())
#endif
                .ConfigureServices((context, services) =>
                {
                    services.AddScoped<IInfoBarService, InfoBarService>();
                    services.AddTransient<INavigationViewService, NavigationViewService>();
                    services.AddTransient<ShellPage>();
                    services.AddTransient<ShellViewModel>();

                    services.AddNavigation(pages => pages
                        .Configure<GamePage>("GamePage")
                        .Configure<SettingsPage>("SettingsPage"));
                })
            );
        MainWindow = builder.Window;

#if DEBUG
        MainWindow.EnableHotReload();
#endif

        Host = builder.Build();
        DefaultScope = Current.Host!.Services.CreateScope();

        if (MainWindow.Content is not ShellPage)
        {
            var shell = Host.Services.GetRequiredService<ShellPage>();
            async void OnShellLoaded(object sender, RoutedEventArgs args)
            {
                await Host.Services.GetRequiredService<INavigationService>().NavigateToAsync("GamePage");
                shell.Loaded -= OnShellLoaded;
            }
            shell.Loaded += OnShellLoaded;
            MainWindow.Content = shell;
        }

        // Ensure the current window is active
        MainWindow.Activate();
    }
}
