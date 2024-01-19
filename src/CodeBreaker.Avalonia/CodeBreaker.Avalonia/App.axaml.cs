using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CodeBreaker.Avalonia.ViewModels;
using CodeBreaker.Avalonia.Views;
using CodeBreaker.Avalonia.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CodeBreaker.Avalonia;
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        var builder = Host.CreateApplicationBuilder(Environment.GetCommandLineArgs());
        _host = builder.Build();
        DefaultScope = _host!.Services.CreateScope();
    }

    internal new static App Current => (App)Application.Current!;

    internal IServiceScope? DefaultScope { get; private set; }

    public T GetService<T>()
        where T : class =>
        DefaultScope!.ServiceProvider.GetRequiredService<T>();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                Content = _host.Services.GetRequiredService<GamePage>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new GamePage();
        }

        base.OnFrameworkInitializationCompleted();
    }
}