using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Services.Navigation
{
    public class PageInfo
    {
        public string Route { get; }
        public Dictionary<string, AdditionalPageInfo> AdditionalInfo { get; }

        public PageInfo(string route, Dictionary<string, AdditionalPageInfo> additionalInfo)
        {
            Route = route;
            AdditionalInfo = additionalInfo;
        }
    }

    public class AdditionalPageInfo
    {
        private object _value;

        public bool IsArray => _value is string[];

        public string Text => (string)_value;
        public string[] Array => (string[])_value;

        public AdditionalPageInfo(string value)
        {
            _value = value;
        }

        public AdditionalPageInfo(string[] value)
        {
            _value = value;
        }
    }
}
