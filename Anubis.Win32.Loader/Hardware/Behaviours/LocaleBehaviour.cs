using Anubis.System;
using System.Globalization;
using System.Threading;

namespace Anubis.Win32.Loader.Hardware.Behaviours
{
    public class LocaleBehaviour : ExBehaviour
    {
        private CultureInfo Culture;

        public override void Awake()
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
