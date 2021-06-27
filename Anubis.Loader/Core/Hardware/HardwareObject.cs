using System.Security.Cryptography;
using System.Text;

namespace Anubis.Loader.Core.Hardware
{
    public class HardwareObject
    {
        private HardwareCollectorComponent HardwareCollector;
        private LocaleHardwareComponent LocaleHardware;

        public static HardwareObject Create()
        {
            var obj = new HardwareObject();
            obj.Awake();

            return obj;
        }

        public void Awake()
        {
            HardwareCollector = new HardwareCollectorComponent();
            LocaleHardware = new LocaleHardwareComponent();

            HardwareCollector.Awake();
            LocaleHardware.Awake();
        }

        public string GetHardwareIdentifier()
            => HardwareCollector.GenerateIdentifier();

        public LocaleHardwareComponent GetLocale()
            => LocaleHardware;

        /// <summary>
        /// aka <see cref="LocaleHardwareComponent.GetShortLocale"/>
        /// </summary>
        /// <returns></returns>
        public string GetTwoLetterLocaleCode()
            => GetLocale().GetShortLocale();

        private string MD5Hash(string str)
            => Encoding.UTF8.GetString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(str)));
    }
}
