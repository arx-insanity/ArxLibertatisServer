using System;
using System.IO;

namespace ArxLibertatisServer.Messages
{
    /// <summary>
    /// received by server when client connects
    /// </summary>
    [MessageType((ushort)MessageTypeEnum.Handshake)]
    public class Handshake : Message
    {
        public string name;
        public Guid id;

        public override void FromBytes(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            name = ReadString(ms);
            id = Guid.Parse(ReadString(ms));
        }

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            Write(name, ms);
            Write(id.ToString(), ms);
            return ms.ToArray();
        }
    }
}
