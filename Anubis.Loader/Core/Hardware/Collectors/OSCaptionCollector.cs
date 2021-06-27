using Anubis.Loader.Core.Hardware.Collectors.Archetype;

using System;

namespace Anubis.Loader.Core.Hardware.Collectors
{
	public class OSCaptionCollector : BaseCaptionCollector
	{
		public OSCaptionCollector()
			: base(new System.Management.ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem"))
		{ }

		public string GetFreePhysicalMemory()
			=> Get<string>("FreePhysicalMemory");

		public string GetMark()
			=> Convert.ToInt32(GetFreePhysicalMemory()) > 7000000 ? "Suitable" : "Non suitable";

		public string GetName()
			=> Get<string>("Name");

		public string GetSerialNumber()
			=> Get<string>("SerialNumber");

		public string GetUser()
			=> Get<string>("RegisteredUser");

		public string GetVersion()
			=> Get<string>("Version");
	}
}
