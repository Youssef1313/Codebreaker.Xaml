using Codebreaker.ViewModels.Contracts.Services;

namespace Codebreaker.MAUI.Views.Pages;

public class TestPage : ContentPage
{
	public TestPage(INavigationService navigationService)
	{
        var button = new Button { Text = "To GamePage" };
        button.Clicked += async (s, e) => await navigationService.NavigateToAsync("GamePage");

        Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!" },
				button
            }
		};
    }
}