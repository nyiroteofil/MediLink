using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;

namespace MediLine_FrontEnd
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionToolkit()
                .ConfigureMauiHandlers(handlers =>
                {
#if IOS || MACCATALYST
    				handlers.AddHandler<Microsoft.Maui.Controls.CollectionView, Microsoft.Maui.Controls.Handlers.Items2.CollectionViewHandler2>();
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                    fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
                });

#if DEBUG
    		builder.Logging.AddDebug();
    		builder.Services.AddLogging(configure => configure.AddDebug());
            Environment.SetEnvironmentVariable("MEDILINK_URL", "https://localhost:7056/api");
#endif

            builder.Services.AddSingleton<ApiHandler>();

            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPatientPage>();
            builder.Services.AddTransient<RequestAppointmentPage>();
            builder.Services.AddTransient<ViewAppointmentsPage>();
            builder.Services.AddTransient<AdminUserManagerPage>();

            return builder.Build();
        }
    }
}
