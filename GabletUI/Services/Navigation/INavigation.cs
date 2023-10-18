using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Services.Navigation
{
    public interface INavigation
    {
        public PageInfo GetPageInfo();
        public void PushRoute(string route);
        public void RegisterRoutePopCallback(Action callback);
    }
}
