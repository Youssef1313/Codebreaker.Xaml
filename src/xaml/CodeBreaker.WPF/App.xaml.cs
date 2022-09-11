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
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.Configure<CodeBreaker6x4ViewModelOptions>(options => options.EnableDialogs = true);
                services.AddTransient<IDialogService, Services.WPFDialogService>();
                services.AddScoped<CodeBreaker6x4ViewModel>();
                services.AddScoped<IAuthService, AuthService>();
                services.AddHttpClient<IGameClient, GameClient>(client =>
                {
                    string uriString = context.Configuration["CodeBreakerAPIURI"] ?? throw new ConfigurationErrorsException("CodeBreakerAPIURI not configured");
                    client.BaseAddress = new Uri(uriString);
                });
            })
            .Build();
    }

    public void Dispose()
    {
        _host.Dispose();
    }

    public IServiceProvider Services => _host.Services;

    protected override void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
    }
}
