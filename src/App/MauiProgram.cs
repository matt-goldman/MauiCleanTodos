using MauiCleanTodos.ApiClient;

namespace MauiCleanTodos.App;

public static partial class MauiProgram
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
				fonts.AddFont("Sora-VariableFont_wght.ttf", "Sora");
				fonts.AddFont("Viga-Regular.ttf", "Viga");
				fonts.AddFont("FluentSystemIcons-Regular.ttf", "FluentIcons");
			});

		var options = new ApiClientOptions
		{
			Authority	= "https://rwds.goforgoldman.com",
			BaseUrl		= "https://rwds.goforgoldman.com",
			ClientId	= "mct-mobile-app",
			RedirectUri	= "auth.com.goldie.mauicleantodos.app://callback",
			Scope		= "MauiCleanTodos.WebUIAPI"
        };

		builder.Services.RegisterApiClient(options);

		UseAutoreg(builder.Services);

		return builder.Build();
	}

	static partial void UseAutoreg(IServiceCollection services);
}