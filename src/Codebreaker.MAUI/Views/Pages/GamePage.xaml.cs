using Codebreaker.ViewModels.Contracts.Services;

namespace Codebreaker.MAUI.Views.Pages;

public partial class GamePage : ContentPage
{
	private readonly INavigationService _navigationService;

	public GamePage(GamePageViewModel viewModel, INavigationService navigationService)
	{
		_navigationService = navigationService;
		BindingContext = viewModel;
		InitializeComponent();

		WeakReferenceMessenger.Default.Register<InfoMessage>(this, async (r, m) =>
		{
			await DisplayAlert("Info", m.Text, "Close");
		});
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await _navigationService.NavigateToAsync("TestPage");
    }
}
