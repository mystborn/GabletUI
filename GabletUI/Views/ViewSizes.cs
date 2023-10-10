using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Views
{
    public static class ViewSizes
    {
        public const int ExtraSmall = 600;
        public const int Small = 768;
        public const int Medium = 992;
        public const int Large = 1200;

        public static void SetupDynamicView(this Control parent, Control? desktop, Control? mobile)
        {
            parent.EffectiveViewportChanged += (s, e) =>
            {
                if ((parent.GetVisualRoot()?.ClientSize.Width ?? Large) < Small)
                {
                    if (mobile is not null)
                        mobile.IsVisible = true;

                    if (desktop is not null)
                        desktop.IsVisible = false;
                }
                else
                {
                    if (mobile is not null)
                        mobile.IsVisible = false;

                    if(desktop is not null)
                        desktop.IsVisible = true;
                }
            };
        }
    }
}
