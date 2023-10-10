using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GabletUI.Api;
using GabletUI.Api.Accounts;
using GabletUI.Navigation;
using GabletUI.Store;
using GabletUI.ViewModels;
using GabletUI.Views;
using Microsoft.Extensions.DependencyInjection;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
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
                .AddSingleton<HttpClient>()
                .AddSingleton<IStorage>(_storageMechanism)
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