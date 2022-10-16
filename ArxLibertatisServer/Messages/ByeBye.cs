using System;

namespace ArxLibertatisServer.Messages
{
    /// <summary>
    /// received by server when client exits
    /// </summary>
    [MessageType((ushort)MessageTypeEnum.ByeBye)]
    public class ByeBye : Message
    {
        public override void FromBytes(byte[] bytes)
        { }

        public override byte[] GetBytes()
        {
            return Array.Empty<byte>();
        }
    }
}
