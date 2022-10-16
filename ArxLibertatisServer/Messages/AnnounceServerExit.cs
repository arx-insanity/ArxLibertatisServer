using System;

namespace ArxLibertatisServer.Messages
{
    /// <summary>
    /// sent by server to tell clients the server exited
    /// </summary>
    [MessageType((ushort)MessageTypeEnum.AnnounceServerExit)]
    public class AnnounceServerExit : Message
    {
        public override void FromBytes(byte[] bytes)
        { }

        public override byte[] GetBytes()
        {
            return Array.Empty<byte>();
        }
    }
}
