using CodeBreaker.ViewModels;

namespace CodeBreaker.MAUI;

public partial class MainPage : ContentPage
{

	public MainPage(CodeBreaker6x4ViewModel viewModel)
	{
		ViewModel = viewModel;

		BindingContext = this;
		InitializeComponent();
	}

	public CodeBreaker6x4ViewModel ViewModel { get; }
}

