using Anubis.Server.Networking.Processors;

using Jareem.Network.Systems.Tcp.Observeable.Providers;
using Jareem.Network.Systems.Tcp.Server;
using Jareem.Support.Implements.Observer.Base;

using System;
using System.Collections.Generic;

namespace Anubis.Server.Networking
{
    public class SubscribersManager
    {
        private List<Action<TcpServer>> Processors = new List<Action<TcpServer>>()
        {
            (service) => service.Subscribe<TcpConnectionProvider, BaseData>(new ConnectionProcessor()),
            (service) => service.Subscribe<TcpReceivingProvider, BaseData>(new AuthorizeProcessor()),
        };

        public void Mount(TcpServer server)
            => Processors.ForEach((item) => item?.Invoke(server));
    }
}
