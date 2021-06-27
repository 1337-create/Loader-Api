using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Network.Security.Ciphers
{
    public class Rijndael : ICryptoCipher
    {
        public string Decrypt( string data, string key )
        {
            return data;
        }

        public string Encrypt( string data, string key )
        {
            return data;
        }
    }
}
