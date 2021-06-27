using Jareem.Network.Systems.Tcp.Observeable.Providers.TcpConnection;
using Jareem.Support.Implements.Observer.Base;

using System;

namespace Anubis.Server.Networking.Processors
{
    public class ConnectionProcessor : IObserver<BaseData>
	{
		public void OnCompleted()
		{
			ConsoleWriter.Success("Network module has been stopped");
		}

		public void OnError(Exception error)
		{
			//ConsoleWriter.Error($"Internal exception in network module:\n {error}");
		}

		public void OnNext(BaseData value)
		{
			if (value == null)
			{
				ConsoleWriter.Error($"Client has been disconnected with internal error");
				return;
			}

			var conn = value.As<TcpConnection>();
			if (conn.NativeClient.Connected)
			{
				ConsoleWriter.Info($"Client: {conn.Identifier} has been accepted");
			}
			else
			{
				ConsoleWriter.Info($"Client: {conn.Identifier} has been disconnected");
			}
		}
	}
}
