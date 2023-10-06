using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Api
{
    public class GabletApiException : Exception
    {
        public IResponse Response { get; }

        public GabletApiException(IResponse response)
        {
            Response = response;
        }
    }
}
