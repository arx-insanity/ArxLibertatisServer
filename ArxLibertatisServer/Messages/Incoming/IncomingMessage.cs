using NLog;
using System;
using System.IO;
using System.Text;

namespace ArxLibertatisServer.Messages.Incoming
{
    public abstract class IncomingMessage : Message
    {
        public abstract void FromBytes(byte[] bytes);

        public static string ReadString(MemoryStream buffer)
        {
            byte[] b = new byte[4];
            buffer.Read(b, 0, 4);
            uint bytesLen = BitConverter.ToUInt32(b);
            b = new byte[bytesLen];
            buffer.Read(b, 0, (int)bytesLen);
            var str = Encoding.UTF8.GetString(b);
            return str;
        }
    }
}
