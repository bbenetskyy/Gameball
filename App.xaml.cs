using Gameball.Services;

namespace Gameball;

public partial class App : Application
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();

		MainPage = new NavigationPage();
		navigationService.NavigateToPage<MainPage>();
	}

	#region Events
	#endregion Events

	#region Fields
	#endregion Fields

	#region Contstoctors
	#endregion Contstoctors

	#region Properties
	#endregion Properties

	#region Commands
	#endregion Commands

	#region Public Methods
	#endregion Public Methods

	#region Protected Methods
	#endregion Protected Methods

	#region Private Methods
	#endregion Private Methods
}
