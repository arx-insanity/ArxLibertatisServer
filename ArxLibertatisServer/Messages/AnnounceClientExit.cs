using System;
using System.IO;

namespace ArxLibertatisServer.Messages
{
    /// <summary>
    /// sent by server to tell clients a client left
    /// </summary>
    [MessageType((ushort)MessageTypeEnum.AnnounceClientExit)]
    public class AnnounceClientExit : Message
    {
        public Guid id;

        public override void FromBytes(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            id = Guid.Parse(ReadString(ms));
        }

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            Write(id.ToString(), ms);
            return ms.ToArray();
        }
    }
}
