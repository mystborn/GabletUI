using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GabletUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace GabletUI.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        this.SetupDynamicView(null, MobileNavBar);
    }

    public void TabIconClicked(object sender, RoutedEventArgs args)
    {
        var control = (StyledElement)sender;
        while (control.Parent is not null && control.Parent is not ItemsControl)
            control = control.Parent;

        if (control.Parent is null)
            return;

        var items = (ItemsControl)control.Parent;
        var vm = (HomeViewModel)DataContext;
        var panel = items.ItemsPanelRoot;
        var index = panel.Children.IndexOf((Control)control);
    }
}