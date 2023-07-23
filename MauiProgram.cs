using Microsoft.Extensions.Logging;
using MCDiscordBot.HelperClasses;
using MCDiscordBot.ServerClasses;
using Microsoft.Maui.LifecycleEvents;

namespace MCDiscordBot;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<ConfigLoader>();
		builder.Services.AddSingleton<ServerManager>();
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<Config>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
