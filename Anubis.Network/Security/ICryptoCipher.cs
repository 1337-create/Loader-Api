namespace Anubis.Network.Security
{
    public interface ICryptoCipher
    {
        string Encrypt( string data, string key );
        string Decrypt( string data, string key );
    }
}
