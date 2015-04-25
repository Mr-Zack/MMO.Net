using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMO.Photon.Server.Application;

namespace MMO.Photon.Server
{
    public abstract class DefaultRequestHandler : PhotonServerHandler
    {
        public DefaultRequestHandler(PhotonApplication application)
            : base(application)
        {

        }
    }
}
