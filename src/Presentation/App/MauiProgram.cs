using CommunityToolkit.Maui;
using MauiCleanTodos.ApiClient;
using MauiCleanTodos.App.Controls;

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
			})
			.UseMauiCommunityToolkit();

        var options = new ApiClientOptions
		{
			Authority	= "https://rwds.goforgoldman.com",
			BaseUrl		= "https://rwds.goforgoldman.com",
			ClientId	= "mctmobileapp",
			RedirectUri	= "auth.com.goldie.mauicleantodos.app://callback",
			Scope		= "MauiCleanTodos.WebUIAPI openid profile offline_access"
        };

		builder.Services.RegisterMauiClient(options);

		builder.Services.AddSingleton<IBottomSheet, BottomSheetControl>();

		UseAutoreg(builder.Services);

		return builder.Build();
	}

	static partial void UseAutoreg(IServiceCollection services);
}