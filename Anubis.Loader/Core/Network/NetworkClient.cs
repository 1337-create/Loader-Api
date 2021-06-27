using Jareem.Network.Packets;
using Jareem.Network.Systems;
using Jareem.Network.Systems.Tcp.Client;
using Jareem.Network.Systems.Tcp.Observeable.Providers.TcpConnection;
using Jareem.Network.Systems.Tcp.Observeable.Providers.TcpReceiving;

using System;
using System.Threading.Tasks;

namespace Anubis.Loader.Core.Network
{
    public class NetworkClient
    {
		private TcpTunnel Service;

		public NetworkClient()
		{
			Service = ClientFactory.CreateTcpClient("194.87.96.24", 8790);
			//Service = ClientFactory.CreateTcpClient("127.0.0.1", 8789);
		}

		public bool Connect()
		{
			try
			{
				Service.Connect();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public void Disconnect()
			=> Service.Disconnect();
		public bool IsConnected()
			=> Service.IsConnected();
		public TcpConnection GetConnection()
			=> Service.GetConnection();

		public T SendAndWait<T>(BaseNetworkable structure) where T : BaseNetworkable
			=> Service.SendAsync<T>(structure, structure.Identifier);

		public TcpNetworkData SendWait(BaseNetworkable structure)
			=> Service.SendAndWait(structure);

		public void Send(BaseNetworkable structure)
			=> Service.Send(structure);

		public TcpTunnel GetService()
			=> Service;
	}
}
