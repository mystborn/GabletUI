﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabletUI.Api
{
    public interface IResponse
    {
        public ErrorResponse? Error { get; set; }
    }
}
