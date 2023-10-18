using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GabletUI.Api;
using GabletUI.Api.Accounts;
using GabletUI.Services.Navigation;
using GabletUI.Services.Store;
using GabletUI.Services.Validation;
using GabletUI.ViewModels;
using GabletUI.Views;
using Microsoft.Extensions.DependencyInjection;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using Serilog;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace GabletUI
{
    public partial class App : Application
    {
        private static IStorage _storageMechanism = new InMemoryStore();
        private static INavigation _navigationService = new DefaultNavigationService();

        public static IServiceProvider Services { get; private set; }

        public static void ConfigureStorage(IStorage storage)
        {
            _storageMechanism = storage;
        }

        public static void ConfigureNavigation(INavigation navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            IconProvider.Current
                .Register<FontAwesomeIconProvider>();

            var services = new ServiceCollection();

            services
                .AddLogging(builder =>
                {
                    var logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        //.WriteTo.Trace()
                        .WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}")
                        .CreateLogger();

                    builder.AddSerilog(logger);
                })
                .AddSingleton<HttpClient>()
                .AddSingleton<IStorage>(_storageMechanism)
                .AddSingleton<IValidationService>(new RegexValidator())
                .AddSingleton(_navigationService)
                .AddSingleton<AuthStore>(provider => {
                    return new AuthStore(provider.GetService<IStorage>()!);
                })
                .AddSingleton<IAccountService>(provider => {
                    var client = provider.GetService<HttpClient>();
                    var store = provider.GetService<AuthStore>();
                    return new AccountService(
                        provider.GetService<HttpClient>()!,
                        provider.GetService<AuthStore>()!);
                    });
            //.AddSingleton<IProfileService>(provider => null);

            services
                .AddTransient<HomeViewModel>()
                .AddTransient<MainViewModel>();

            Services = services.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = Services.GetService<MainViewModel>()
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = Services.GetService<MainViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}