using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMO.Photon.Server.Application;

namespace MMO.Photon.Server
{
    public abstract class DefaultResponseHandler : PhotonServerHandler
    {
        protected DefaultResponseHandler(PhotonApplication application)
            : base(application)
        {

        }
    }
}
