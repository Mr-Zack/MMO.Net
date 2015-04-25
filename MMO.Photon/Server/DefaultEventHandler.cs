using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMO.Photon.Server.Application;

namespace MMO.Photon.Server
{
    public abstract class DefaultEventHandler : PhotonServerHandler
    {
        public DefaultEventHandler(PhotonApplication application)
            : base(application)
        {

        }
    }
}
