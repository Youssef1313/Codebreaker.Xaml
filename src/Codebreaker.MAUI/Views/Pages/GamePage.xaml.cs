using Codebreaker.ViewModels.Contracts.Services;

namespace Codebreaker.MAUI.Views.Pages;

public partial class GamePage : ContentPage
{
	private readonly INavigationService _navigationService;

	public GamePage(GamePageViewModel viewModel, INavigationService navigationService)
	{
		ViewModel = viewModel;
		_navigationService = navigationService;

		BindingContext = this;
		InitializeComponent();

		WeakReferenceMessenger.Default.Register<InfoMessage>(this, async (r, m) =>
		{
			await DisplayAlert("Info", m.Text, "Close");
		});
	}

	public GamePageViewModel ViewModel { get; }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await _navigationService.NavigateToAsync("TestPage");
    }
}
