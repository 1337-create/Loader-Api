using Anubis.Win32.Loader.Hardware.Behaviours.Collectors.Archetype;
using System.Net.NetworkInformation;
using SysManagement = System.Management;

namespace Anubis.Win32.Loader.Hardware.Behaviours.Collectors
{
    public class NetworkCaptionCollector : BaseCaptionCollector
    {
        public NetworkCaptionCollector()
            : base( new SysManagement.ManagementObjectSearcher( "root\\CIMV2", "SELECT * FROM Win32_NetworkAdapterConfiguration" ) )
        { }

        public string GetIpGateway()
            => Get<string>( "DefaultIPGateway" );

        public string GetIpAddress()
            => Get<string>( "IPAddress" );

        public string GetIpSubnet()
            => Get<string>( "IPSubnet" );

        public string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach ( NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces() )
            {
                if ( nic.OperationalStatus == OperationalStatus.Up )
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }

            return macAddresses;
        }

        public string GetServiceName()
            => Get<string>( "ServiceName" );
    }
}
