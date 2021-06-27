using Anubis.Network;
using Anubis.Network.Client;
using Anubis.System;

using System;

namespace Anubis.Win32.Loader.Network
{
    public class NetworkExObject : ExObject
    {
        private TcpSubscriber Service;

        public NetworkExObject()
            : base()
        {
            /**
             * Addresses:
             * 194.87.96.24 - Switzerland
             * -----------
             * Ports:
             * 8789 - PUBG
             * 8790 - RUST
             */
            Service = NetworkFactory.CreateRemoteSubscriber( "194.87.96.24", 8790 );
        }

        public bool Connect()
        {
            try
            {
                Service.Connect();
                return true;
            }
            catch ( Exception )
            {
                return false;
            }
        }
        public void Disconnect()
            => Service.Disconnect();
        public bool IsConnected()
            => Service.IsConnected();

        public T SendAsync<T>( BaseNetworkEntity entity ) where T : BaseNetworkEntity, new()
            => Service.SendAsync<T>( entity, BaseNetworkEntity.GetIdentifierByType<T>() );

        public void Send( BaseNetworkEntity entity )
            => Service.Send( entity );

        public TcpSubscriber GetService()
            => Service;
    }
}
