using Microsoft.EntityFrameworkCore.Internal;

using System.Collections.Generic;
using System.Linq;

namespace Anubis.Win32.Server
{
    public enum ArgKey
    {
        //Database
        DatabaseHost,
        DatabaseUser,
        DatabasePassword,
        DatabaseReference,

        //Network
        NetworkHost,
        NetworkPort,
        NetworkEncryption,
        NetworkLink
    }

    public class Args
    {
        private string[] m_Args;
        private Dictionary<string, string> m_ArgsValues;

        public Args(string[] args)
        {
            m_Args = args;
            m_ArgsValues = new Dictionary<string, string>();

            Parse();
        }
        public string GetArgValue( ArgKey key )
        {
            var first = m_ArgsValues.FirstOrDefault( ( x ) => x.Key.ToLower() == key.ToString().ToLower() );
            if(first == default)
            {
                return first.Value;
            }

            return null;
        }

        private void Parse()
        {
            foreach(var item in m_Args)
            {
                var parsed = ParseArg( item.Replace( "-", "" ) );
                m_ArgsValues.Add( parsed.Key, parsed.Value );
            }
        }
        private KeyValuePair<string, string> ParseArg(string arg)
        {
            var splitted = arg.Split( '=' );
            return new KeyValuePair<string, string>( splitted[ 0 ], splitted[ 1 ] );
        }
    }
}
