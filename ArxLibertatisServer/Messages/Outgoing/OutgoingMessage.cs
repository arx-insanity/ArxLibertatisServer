using System;
using System.IO;
using System.Text;

namespace ArxLibertatisServer.Messages.Outgoing
{
    public abstract class OutgoingMessage : Message
    {
        public abstract byte[] GetBytes();

        public static void Write(string str, MemoryStream buffer)
        {
            byte[] strBytes = Encoding.UTF8.GetBytes(str);
            uint bytesLen = (uint)strBytes.Length;
            byte[] bytesLenBytes = BitConverter.GetBytes(bytesLen);
            buffer.Write(bytesLenBytes);
            buffer.Write(strBytes);
        }
    }
}
