using Codebreaker.MAUI.Services.Navigation;
using Codebreaker.ViewModels.Contracts.Services;
using Microsoft.Extensions.Configuration;

namespace Codebreaker.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
#if DEBUG
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");
#else
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Production");
#endif

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Configuration.AddJsonStream(FileSystem.OpenAppPackageFileAsync("appsettings.json").Result);
        builder.Configuration.AddJsonStream(FileSystem.OpenAppPackageFileAsync($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json").Result);
        builder.Services.Configure<GamePageViewModelOptions>(options => { });
		builder.Services.AddNavigation<MauiNavigationService>(pages => pages
			.Configure("GamePage", "//Main/GamePage")
			.Configure("TestPage", "//Main/TestPage"));
		builder.Services.AddScoped<IDialogService, MauiDialogService>();
		builder.Services.AddScoped<IInfoBarService, InfoBarService>();
        builder.Services.AddScoped<GamePageViewModel>();
		builder.Services.AddHttpClient<IGamesClient, GamesClient>(client =>
		{
			client.BaseAddress = new(builder.Configuration["ApiBase"] ??
				throw new InvalidOperationException("Could not find ApiBase configuration"));
		});
		builder.Services.AddTransient<GamePage>();
		builder.Services.AddTransient<TestPage>();
		return builder.Build();
	}
}
