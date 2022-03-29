using Gameball.PageModels;

namespace Gameball.Pages;

public partial class ReferralPage : ContentPage
{
	public ReferralPage(ReferralPageModel pageModel)
	{
		BindingContext = pageModel;
		InitializeComponent();
	}
}

