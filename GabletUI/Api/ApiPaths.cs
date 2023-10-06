using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Api
{
    public class ApiPaths
    {
#if DEBUG
        public const string API_SERVER = "http://127.0.0.1:3000";
        public const string AUTH_SERVER = "http://127.0.0.1:3030";
#endif
    }
}
