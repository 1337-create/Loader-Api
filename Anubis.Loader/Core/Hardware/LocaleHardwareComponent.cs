using System.Globalization;
using System.Threading;

namespace Anubis.Loader.Core.Hardware
{
    public class LocaleHardwareComponent
    {
        private CultureInfo Culture;

        public void Awake()
        {
            Culture = Thread.CurrentThread.CurrentCulture;
        }

        public CultureInfo GetCulture()
            => Culture;

        public string GetShortLocale()
            => Culture.TwoLetterISOLanguageName;

        public int GetLocaleCode()
            => Culture.LCID;
    }
}
