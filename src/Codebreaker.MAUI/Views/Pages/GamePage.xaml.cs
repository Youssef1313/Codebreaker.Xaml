using Codebreaker.ViewModels.Contracts.Services;

namespace Codebreaker.MAUI.Views.Pages;

public partial class GamePage : ContentPage, IRecipient<GameMoveMessage>, IRecipient<InfoMessage>
{
	private readonly INavigationService _navigationService;

	public GamePage(GamePageViewModel viewModel, INavigationService navigationService)
	{
		_navigationService = navigationService;
		BindingContext = viewModel;
		InitializeComponent();
        WeakReferenceMessenger.Default.RegisterAll(this);
	}

    public async void Receive(GameMoveMessage message)
    {
		if (message.GameMoveValue is not GameMoveValue.Completed)
			return;

        await Task.Delay(300);
        await pegScrollView.ScrollToAsync(0, pegScrollView.ContentSize.Height, true);
    }

    public async void Receive(InfoMessage message)
    {
        await DisplayAlert("Info", message.Text, "Close");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await _navigationService.NavigateToAsync("TestPage");
    }
}
