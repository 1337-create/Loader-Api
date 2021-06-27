using Anubis.System;

using System;
using System.Text;

namespace Anubis.Win32.Loader.Security
{
    public class SecurityExObject : ExObject
    {
        public string ParseRegKey(string register)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes( register );
            return Convert.ToBase64String( plainTextBytes );
        }
        public string ParseHDD(string drive)
        {
            var base64EncodedBytes = Convert.FromBase64String( drive );
            return Encoding.UTF8.GetString( base64EncodedBytes );
        }
    }
}
