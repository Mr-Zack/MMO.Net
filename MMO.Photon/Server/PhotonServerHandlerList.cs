using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMO.Framework;
using ExitGames.Logging;

namespace MMO.Photon.Server
{
    public class PhotonServerHandlerList
    {
        private readonly DefaultRequestHandler _defaultRequestHandler;
        private readonly DefaultResponseHandler _defaultResponseHandler;
        private readonly DefaultEventHandler _defaultEventHandler;
        protected readonly ILogger Log = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<int, PhotonServerHandler> _requestHandlerList;
        private readonly Dictionary<int, PhotonServerHandler> _responseHandlerList;
        private readonly Dictionary<int, PhotonServerHandler> _eventHandlerList;
        
        public PhotonServerHandlerList(IEnumerable<IHandler<PhotonServerHandler>> handlers, DefaultRequestHandler defaultRequestHandler, DefaultResponseHandler defaultResponseHandler, DefaultEventHandler defaultEventHandler)
        {
            _defaultRequestHandler = defaultRequestHandler;
            _defaultResponseHandler = defaultResponseHandler;
            _defaultEventHandler = defaultEventHandler;

            _requestHandlerList = new Dictionary<int,PhotonServerHandler>();
            _responseHandlerList = new Dictionary<int, PhotonServerHandler>();
            _eventHandlerList = new Dictionary<int, PhotonServerHandler>();

            foreach(PhotonServerHandler handler in handlers)
            {
                if(!RegisterHandler(hander))
                {
                    Log.WarnFormat("Attempted to regiester handler {0} for type {1} {2}", handler.GetType().Name, handler.Type, handler.Code);
                }
            }
        }

        public bool RegisterHandler(PhotonServerHandler handler)
        {
            var registered = false;

            if( (handler.Type & MessageType.Request) == MessageType.Request )
            {
                if(handler.SubCode.HasValue && !_requestHandlerList.ContainsKey(handler.SubCode.Value))
                {
                    _requestHandlerList.Add(handler.SubCode, handler);
                    registered = true;
                }
                else if(_requestHandlerList.ContainsKey(handler.Code))
                {
                    _requestHandlerList.Add(handler.Code, handler);
                    registered = true;
                }
                else
                {
                    Log.ErrorFormat"RequstHandler list alreay, contains handler for {0} - cannot add {1}", handler.Code, handler.GetType().Name);
                }
            }

            if( (handler.Type & MessageType.Response) == MessageType.Response )
            {
                if(handler.SubCode.HasValue && !_responseHandlerList.ContainsKey(handler.SubCode.Value))
                {
                    _responseHandlerList.Add(handler.SubCode, handler);
                    registered = true;
                }
                else if(_responseHandlerList.ContainsKey(handler.Code))
                {
                    _responseHandlerList.Add(handler.Code, handler);
                    registered = true;
                }
                else
                {
                    Log.ErrorFormat"RequstHandler list alreay, contains handler for {0} - cannot add {1}", handler.Code, handler.GetType().Name);
                }
            }

            if( (handler.Type & MessageType.Async) == MessageType.Async )
            {
                if(handler.SubCode.HasValue && !_eventHandlerList.ContainsKey(handler.SubCode.Value))
                {
                    _eventHandlerList.Add(handler.SubCode, handler);
                    registered = true;
                }
                else if(_eventHandlerList.ContainsKey(handler.Code))
                {
                    _eventHandlerList.Add(handler.Code, handler);
                    registered = true;
                }
                else
                {
                    Log.ErrorFormat"RequstHandler list alreay, contains handler for {0} - cannot add {1}", handler.Code, handler.GetType().Name);
                }
            }

            return registered;
        }

        public bool HandlerMessage(IMessage message, PhotonServerPeer peer)
        {
            bool handled = false;

            switch(message.Type)
            {
                case MessageType.Request:
                    if(message.SubCode.HasValue && _requestHandlerList.ContainsKey(message.SubCode.Value))
                    {
                        _requestHandlerList[message.SubCode.Value].HandleMessage(message, peer);
                        handled = true;
                    } 
                    else if(!message.SubCode.HasValue && _requestHandlerList.ContainsKey(message.Code))
                    {
                        _requestHandlerList[message.Code].HandleMessage(message, peer);
                        handled = true;
                    }
                    else
                    {
                        _defaultRequestHandler.HandleMessage(message, peer);
                    }
                    break;
            }

            return handled;
        }
    }
}
