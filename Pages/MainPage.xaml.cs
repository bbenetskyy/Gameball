using Gameball.PageModels;
namespace Gameball;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageModel pageModel)
	{
		BindingContext = pageModel;
		InitializeComponent();
	}
}

