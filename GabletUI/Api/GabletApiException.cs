using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Api
{
    public class GabletApiException : Exception
    {
        public ErrorResponse Response { get; }

        public GabletApiException(ErrorResponse response)
            : base(response.ErrorMessage)
        {
            Response = response;
        }
    }
}
