using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMO.Framework
{
    public interface IHandlerList<T>
    {
        bool RegisterHandler(IHandler<T> handler);
        bool HandlerMessage(IMessage message, T peer);
    }
}
