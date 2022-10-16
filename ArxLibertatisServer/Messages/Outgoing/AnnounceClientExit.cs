using System;
using System.IO;

namespace ArxLibertatisServer.Messages.Outgoing
{
    /// <summary>
    /// sent by server to tell clients a client left
    /// </summary>
    [MessageType(MessageTypeEnum.AnnounceClientExit)]
    public class AnnounceClientExit : OutgoingMessage
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
