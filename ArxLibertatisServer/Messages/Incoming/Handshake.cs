using System;
using System.IO;

namespace ArxLibertatisServer.Messages.Incoming
{
    /// <summary>
    /// received by server when client connects
    /// </summary>
    [MessageType(MessageTypeEnum.Handshake)]
    public class Handshake : IncomingMessage
    {
        public string name;

        public override void FromBytes(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            name = ReadString(ms);
        }
    }
}
