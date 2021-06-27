namespace Anubis.Network.Packetize.Authorize
{
    public class AuthorizeNetworkEntity : BaseNetworkEntity
    {
        public string Query { get; set; }
        public string ResultMessage { get; set; }
        public string Link { get; set; }
        public int Seconds { get; set; }
        public string Key { get; set; }

        public AuthorizeNetworkEntity()
            : base(0xCAFE)
        { }

        public override string ToNetworkString()
        {
            return $"{Query}|SERIALIZED";
        }
    }
}
