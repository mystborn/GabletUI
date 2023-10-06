using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Navigation
{
    public class DefaultNavigationService : INavigation
    {
        public PageInfo GetPageInfo()
        {
            return new("/", new());
        }

        public void PushRoute(string route)
        {
        }

        public void RegisterRoutePopCallback(Action callback)
        {
        }
    }
}
