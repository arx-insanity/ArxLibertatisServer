using System;
using System.IO;

namespace ArxLibertatisServer.Messages.Outgoing
{
    /// <summary>
    /// sent by server to accounce when a client joins the server
    /// </summary>
    [MessageType(MessageTypeEnum.AnnounceClientEnter)]
    public class AnnounceClientEnter : OutgoingMessage
    {
        public string name;
        public Guid id;

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            Write(name, ms);
            Write(id.ToString(), ms);
            return ms.ToArray();
        }
    }
}
