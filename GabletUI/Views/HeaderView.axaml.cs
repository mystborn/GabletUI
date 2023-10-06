using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GabletUI.Views;

public partial class HeaderView : UserControl
{
    public HeaderView()
    {
        InitializeComponent();
        this.SetupDynamicView(DesktopView, MobileView);
    }
}