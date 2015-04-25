using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.SocketServer.ServerToServer;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using MMO.Photon.Server.Application;
using MMO.Photon.Application;

namespace MMO.Photon.Server
{
    public class PhotonServerPeer : ServerPeerBase
    {
        private readonly PhotonServerHandlerList _handlerList;
        protected readonly PhotonApplication Server;

        public delegate PhotonServerPeer Factory(IRpcProtocol protocol, IPhotonPeer phototPeer);

        public PhotonServerPeer(IRpcProtocol protocol, IPhotonPeer photonPeer, PhotonServerHandlerList handlerList, PhotonApplication application)
            :base(protocol, photonPeer)
        {
            _handlerList = handlerList;
            Server = application;
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            _handlerList.HandleMessager(new PhotonEvent(eventData.Code, eventData.Parameters.ContainsKey(Server.SubCodeParameterKey) ? (int?)Convert.ToInt32(eventData.Parameters[Server.SubCodeParameterCode]) : null, eventData.Parameters, this));
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            _handlerList.HandleMessager(new PhotonResponse(operationResponse.OperationCode, operationResponse.Parameters.ContainsKey(Server.SubCodeParameterKey)?(int?)Convert.ToInt32(operationResponse.Parameters[Server.SubCodeParameterCode]):null, operationResponse, this));
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            throw new NotImplementedException();
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            _handlerList.HandleMessager(new PhotonRequest(operationRequest.OperationCode, operationRequest.Parameters.ContainsKey(Server.SubCodeParameterKey) ? (int?)Convert.ToInt32(operationRequest.Parameters[Server.SubCodeParameterCode]) : null, operationRequest, this));
        }
    }
}
