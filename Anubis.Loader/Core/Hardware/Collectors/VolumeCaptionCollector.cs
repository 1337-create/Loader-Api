using Anubis.Loader.Core.Hardware.Collectors.Archetype;

namespace Anubis.Loader.Core.Hardware.Collectors
{
    public class VolumeCaptionCollector : BaseCaptionCollector
    {
        public VolumeCaptionCollector()
            : base(new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive"))
        { }

        public string GetInterfaceType()
            => Get<string>("InterfaceType");

        public string GetManufacturer()
            => Get<string>("Manufacturer");

        public string GetModel()
            => Get<string>("Model");

        public string GetSerialNumber()
            => Get<string>("SerialNumber");
    }
}
