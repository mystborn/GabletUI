using GabletUI.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Web;

namespace GabletUI.Browser
{
    public partial class BrowserNavigationService : INavigation
    {
        [JSImport("globalThis.gabletPushUrl")]
        public static partial void PushUrl(string url);

        [JSImport("globalThis.gabletOnPopUrl")]
        public static partial void RegisterPopUrlCallback([JSMarshalAs<JSType.Function>] Action callback);

        [JSImport("globalThis.gabletGetPageParams")]
        public static partial string GetUrlParams();

        public PageInfo GetPageInfo()
        {
            var location = GetUrlParams();

            var url = new Uri(location);
            var urlParams = HttpUtility.ParseQueryString(url.Query);
            var additionalInfo = new Dictionary<string, AdditionalPageInfo>();
            foreach (var urlParam in urlParams.AllKeys)
            {
                var values = urlParams.GetValues(urlParam);
                var info = values.Length switch
                {
                    1 => new AdditionalPageInfo(values[0]),
                    _ => new AdditionalPageInfo(values),
                };
                additionalInfo.Add(urlParam, info);
            }

            return new PageInfo(url.AbsolutePath.TrimStart('/'), additionalInfo);
        }

        public void PushRoute(string route)
        {
            PushUrl(route);
        }

        public void RegisterRoutePopCallback(Action callback)
        {
            RegisterPopUrlCallback(callback);
        }
    }
}
