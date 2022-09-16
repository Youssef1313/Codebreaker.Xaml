using CodeBreaker.MAUI.Services;
using CodeBreaker.ViewModels;

using CommunityToolkit.Mvvm.Messaging;

namespace CodeBreaker.MAUI;

public partial class MainPage : ContentPage
{

	public MainPage(CodeBreaker6x4ViewModel viewModel)
	{
		ViewModel = viewModel;

		BindingContext = this;
		InitializeComponent();

		WeakReferenceMessenger.Default.Register<InfoMessage>(this, async (r, m) =>
		{
			await DisplayAlert("Info", m.Text, "Close");
		});
	}

	public CodeBreaker6x4ViewModel ViewModel { get; }
}
