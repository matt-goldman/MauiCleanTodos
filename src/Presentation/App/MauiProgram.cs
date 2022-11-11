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

		builder.Services.RegisterMauiClient(opt =>
		{
            opt.Authority	= "https://6149-159-196-124-207.ngrok.io";
            opt.BaseUrl		= "https://6149-159-196-124-207.ngrok.io";
            opt.ClientId	= "mctmobileapp";
            opt.RedirectUri = "auth.com.goldie.mauicleantodos.app://callback";
            opt.Scope		= "MauiCleanTodos.WebUIAPI openid profile offline_access";
        });

		builder.Services.AddSingleton<IBottomSheet, BottomSheetControl>();

		UseAutoreg(builder.Services);

		return builder.Build();
	}

	static partial void UseAutoreg(IServiceCollection services);
}