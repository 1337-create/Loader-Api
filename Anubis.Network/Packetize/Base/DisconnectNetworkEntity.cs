namespace Anubis.Network.Packetize.Base
{
    internal class DisconnectNetworkEntity : BaseNetworkEntity
    {
        public string Reason { get; set; }

        public DisconnectNetworkEntity()
            : base(0xDEAD)
        { }
    }
}
