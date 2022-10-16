using System;
using System.IO;

namespace ArxLibertatisServer.Messages.Outgoing
{
    /// <summary>
    /// sent by server to tell client its id
    /// </summary>
    [MessageType(MessageTypeEnum.HandshakeAnswer)]
    public class HandshakeAnswer : OutgoingMessage
    {
        public Guid id;

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            Write(id.ToString(), ms);
            return ms.ToArray();
        }
    }
}
