using Gameball.PageModels;
using Gameball.Services;
using CommunityToolkit.Maui;

namespace Gameball;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("icomoon.ttf", "IcoMoon");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
			})
			.UseMauiCommunityToolkit();

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<MainPageModel>();

		builder.Services.AddTransient<ReferralPage>();
		builder.Services.AddTransient<ReferralPageModel>();

		builder.Services.AddSingleton<INavigationService, NavigationService>();

		return builder.Build();
	}
}
