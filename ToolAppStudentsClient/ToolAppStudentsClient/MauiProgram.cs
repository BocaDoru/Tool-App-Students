using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ToolAppStudentsClient.Popups;
using ToolAppStudentsClient.Services;
using ToolAppStudentsClient.View;
using ToolAppStudentsClient.ViewModel;

namespace ToolAppStudentsClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IPlatformHttpMessageHandler>(sp =>
            {
#if ANDROID
                return new AndroidHttpMessageHandler();
#elif WINDOWS
                return new WindowsHttpMessageHandler();
#else
                return null!;
#endif
            });

            builder.Services.AddHttpClient("custom-httpclient", httpClient =>
            {
                var baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7148" : "https://localhost:7148";
                httpClient.BaseAddress = new Uri(baseAddress);
            }).ConfigureHttpMessageHandlerBuilder(configBuilder =>
            {
                var platformMessageHandler = configBuilder.Services.GetRequiredService<IPlatformHttpMessageHandler>();
                configBuilder.PrimaryHandler = platformMessageHandler.CreateHttpMessageHandler();
            });

            builder.Services.AddSingleton<ClientServices>();
            builder.Services.AddSingleton<CalendarServices>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<WeekCalendar>();
            builder.Services.AddSingleton<WeekCalendarViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}