using Anubis.Network;
using Anubis.Network.Server;
using Anubis.System;

namespace Anubis.Win32.Server.Network
{
    public class NetworkExObject : ExObject
    {
        private TcpObserver m_Service;
        public string Link { get; private set; }

        public NetworkExObject()
            : base()
        {
            var args = ServerContext.GetArgs();
            m_Service = NetworkFactory.CreateRemoteObserver(
                args.GetArgValue( ArgKey.NetworkHost ),
                int.Parse( args.GetArgValue( ArgKey.NetworkPort ) ),
                bool.Parse( args.GetArgValue( ArgKey.NetworkEncryption ) ) );

            SetupLink( args.GetArgValue( ArgKey.NetworkLink ) );
        }

        public void SetupLink( string link )
            => Link = link;

        public void Listen()
            => m_Service.Listen();

        public void Shutdown()
            => m_Service.Shutdown();

        public TcpObserver GetService()
            => m_Service;
    }
}
