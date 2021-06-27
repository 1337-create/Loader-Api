using Anubis.System;
using Anubis.System.Attributes;
using Anubis.Win32.Loader.Hardware.Behaviours;

using System.Security.Cryptography;
using System.Text;

namespace Anubis.Win32.Loader.Hardware
{
    [RequiredBehaviour(typeof(CollectorBehaviour))]
    [RequiredBehaviour(typeof(LocaleBehaviour))]
    public class HardwareExObject : ExObject
    {
        public string GetHardwareIdentifier()
           => MD5Hash( GetComponent<CollectorBehaviour>().GenerateIdentifier() );

        public LocaleBehaviour GetLocale()
            => GetComponent<LocaleBehaviour>();

        public string GetTwoLetterLocaleCode()
            => GetLocale().GetShortLocale();

        private string MD5Hash( string str )
            => Encoding.UTF8.GetString( new MD5CryptoServiceProvider().ComputeHash( Encoding.UTF8.GetBytes( str ) ) );
    }
}
