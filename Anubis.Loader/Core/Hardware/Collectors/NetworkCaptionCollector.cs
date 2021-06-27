using Anubis.Loader.Core.Hardware.Collectors.Archetype;
using System.Net.NetworkInformation;

namespace Anubis.Loader.Core.Hardware.Collectors
{
	public class NetworkCaptionCollector : BaseCaptionCollector
	{
		public NetworkCaptionCollector()
			: base(new System.Management.ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapterConfiguration"))
		{ }

		public string GetIpGateway()
			=> Get<string>("DefaultIPGateway");

		public string GetIpAddress()
			=> Get<string>("IPAddress");

		public string GetIpSubnet()
			=> Get<string>("IPSubnet");

		public string GetMacAddress()
		{
			string macAddresses = string.Empty;

			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (nic.OperationalStatus == OperationalStatus.Up)
				{
					macAddresses += nic.GetPhysicalAddress().ToString();
					break;
				}
			}

			return macAddresses;
		}

		public string GetServiceName()
			=> Get<string>("ServiceName");
	}
}
