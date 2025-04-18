﻿using Labor_Tracker.Services;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace Labor_Tracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddMauiBlazorWebView();

            // Firebase Service
            IServiceCollection serviceCollection = builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = "",
                AuthDomain = "",
                Providers = new Firebase.Auth.Providers.FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }

            }));

            // Register platform-specific implementations
#if ANDROID
            builder.Services.AddSingleton<IDeviceService, Platforms.Android.DeviceService>();
#elif IOS
            builder.Services.AddSingleton<IDeviceService, Platforms.iOS.DeviceService>();

#endif

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
