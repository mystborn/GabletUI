using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using GabletUI;
using GabletUI.Browser;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Threading.Tasks;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine(BrowserNavigationService.GetUrlParams());

        App.ConfigureStorage(new WebStorage());
        App.ConfigureNavigation(new BrowserNavigationService());

        await BuildAvaloniaApp()
            .WithInterFont()
            .UseReactiveUI()
            .StartBrowserAppAsync("out", new BrowserPlatformOptions());
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}