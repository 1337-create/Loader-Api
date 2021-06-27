using Anubis.Network.Settings;
using System.Net;

namespace Anubis.Network.Server
{
    public class TcpObserverSettings : TcpBaseSettings
    {
        public int MaxConnections { get; private set; }
        
        public TcpObserverSettings SetMaxConnections(int maxConnections)
        {
            MaxConnections = maxConnections;
            return this;
        }
    }
}
