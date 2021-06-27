using Jareem.Network.Systems;
using Jareem.Network.Systems.Tcp.Server;

namespace Anubis.Server.Networking
{
    public class NetworkServer
    {
        private static NetworkServer Instance;

        public static NetworkServer GetInstance()
        {
            if (Instance == null)
                Instance = new NetworkServer();

            return Instance;
        }

		private TcpServer Service;
		private SubscribersManager Manager;

		public NetworkServer()
		{
			Service = ServerFactory.CreateTcpServer("194.87.96.24", 8790);
			//Service = ServerFactory.CreateTcpServer("127.0.0.1", 8789);
			Manager = new SubscribersManager();
		}

		public void Initialize()
		{
			Manager.Mount(Service);
			Service.Start(50);
		}

		public TcpServer GetService()
			=> Service;
	}
}
