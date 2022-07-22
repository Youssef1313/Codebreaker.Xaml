using CommunityToolkit.Maui;
using CodeBreaker.ViewModels;
using CodeBreaker.ViewModels.Services;
using CodeBreaker.MAUI.Services;
using CodeBreaker.Services;

namespace CodeBreaker.MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.Configure<CodeBreaker6x4ViewModelOptions>(
			options => options.EnableDialogs = true);
		builder.Services.AddTransient<CodeBreaker6x4ViewModel>();
		builder.Services.AddScoped<IDialogService, MauiDialogService>();
		builder.Services.AddHttpClient<IGameClient, GameClient>(client =>
		{
			client.BaseAddress = new("https://codebreakerapi.purplebush-9a246700.westeurope.azurecontainerapps.io");
		});
		builder.Services.AddTransient<MainPage>();
		return builder.Build();
	}
}
