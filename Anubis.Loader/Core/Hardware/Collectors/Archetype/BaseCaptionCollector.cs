using System.Management;

namespace Anubis.Loader.Core.Hardware.Collectors.Archetype
{
    public abstract class BaseCaptionCollector
    {
        protected ManagementObjectSearcher SearcherInstance;

        public BaseCaptionCollector(ManagementObjectSearcher obj)
        {
            SearcherInstance = obj;
        }

        public T Get<T>(string key)
        {
            foreach (var baseObject in SearcherInstance.Get())
            {
                var managementObject = (ManagementObject)baseObject;
                if (managementObject != null)
                {
                    try
                    {
                        return (T)managementObject[key];
                    }
                    catch
                    {
                        return default;
                    }
                }
            }

            return default;
        }

        public ManagementObjectSearcher GetSearcher()
            => SearcherInstance;
    }
}
