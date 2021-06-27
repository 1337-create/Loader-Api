using Anubis.Network.Client;
using Anubis.Network.Server;

namespace Anubis.Network
{
    public static class NetworkFactory
    {
        public static TcpObserver CreateLocalhostObserver(int port)
        {
            return new TcpObserver( (TcpObserverSettings)new TcpObserverSettings().SetIpAddress( "127.0.0.1" ).SetPort( port ) );
        }
        public static TcpObserver CreateRemoteObserver(string ip, int port, bool encryption = true, int maxConnections = 50)
        {
            var settings = ( TcpObserverSettings )new TcpObserverSettings()
                .SetMaxConnections( maxConnections )
                .SetIpAddress( ip )
                .SetPort( port )
                .Crypto(encryption);


            return new TcpObserver(settings);
        }
        public static TcpObserver CreateRemoteObserverCustom(TcpObserverSettings settings)
        {
            return new TcpObserver( settings );
        }

        public static TcpSubscriber CreateLocalhostSubscriber(int port)
        {
            return new TcpSubscriber( ( TcpSubscriberSettings )new TcpSubscriberSettings().SetIpAddress( "127.0.0.1" ).SetPort( port ) );
        }
        public static TcpSubscriber CreateRemoteSubscriber( string ip, int port, bool encryption = true )
        {
            return new TcpSubscriber( ( TcpSubscriberSettings )new TcpSubscriberSettings().SetIpAddress( ip ).SetPort( port ).Crypto(encryption) );
        }
        public static TcpSubscriber CreateRemoteSubscriberCustom(TcpSubscriberSettings settings)
        {
            return new TcpSubscriber( settings );
        }
    }
}
