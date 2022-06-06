using CodeBreaker.ViewModels;
using CodeBreaker.ViewModels.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Windows;

namespace CodeBreaker.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public sealed partial class App : Application, IDisposable
{
    private IHost _host;
    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddTransient<IDialogService, Services.WPFDialogService>();
                services.AddScoped<CodeBreaker6x4ViewModel>();
                services.AddHttpClient<GameClient>(client =>
                {
                    client.BaseAddress = new Uri("https://codebreakerapi.purplebush-9a246700.westeurope.azurecontainerapps.io");
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
