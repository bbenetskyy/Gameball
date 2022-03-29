using Gameball.PageModels;
namespace Gameball.Pages;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageModel pageModel)
	{
		BindingContext = pageModel;
		InitializeComponent();
	}
}

