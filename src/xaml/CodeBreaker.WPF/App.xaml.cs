using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.ViewModels;
using CodeBreaker.ViewModels.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Configuration;
using System.Windows;

namespace CodeBreaker.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application, IDisposable
{
    private readonly IHost _host;

    public App()
    {
#if DEBUG
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");
#else
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Production");
#endif

        _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(options =>
            {
                string? test = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<CodeBreaker6x4ViewModelOptions>(options => options.EnableDialogs = true);
                services.AddTransient<IDialogService, Services.WPFDialogService>();
                services.AddScoped<CodeBreaker6x4ViewModel>();
                services.AddScoped<IAuthService, AuthService>();
                services.AddHttpClient<IGameClient, GameClient>(client =>
                {
                    string uriString = context.Configuration["ApiBase"] ?? throw new ConfigurationErrorsException("ApiBase not configured");
                    client.BaseAddress = new Uri(uriString);
                });
            })
            .Build();

        DefaultScope = _host.Services.CreateScope();
    }

    public void Dispose()
    {
        DefaultScope.Dispose();
        _host.Dispose();
    }

    public IServiceScope DefaultScope { get; private init; }

    public IServiceProvider Services => _host.Services;

    public T GetService<T>()
        where T : class =>
        DefaultScope.ServiceProvider.GetRequiredService<T>();

    protected override void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
    }
}
