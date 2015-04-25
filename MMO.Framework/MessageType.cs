using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMO.Framework
{
    [Flags]
    public enum MessageType
    {
         Request    = 1<<0
        ,Response   = 1<<1
        ,Async      = 1<<2
    }
}