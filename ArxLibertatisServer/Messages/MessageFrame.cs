using System.IO;

namespace ArxLibertatisServer.Messages
{
    public static class MessageFrame
    {
        public static void Send(Message message, BinaryWriter writer)
        {
            byte[] messageBytes = message.GetBytes();
            uint messageLength = (uint)messageBytes.Length;
            ushort messageType = message.GetMessageType();

            lock (writer.BaseStream)
            {
                writer.Write(messageType);
                writer.Write(messageLength);
                writer.Write(messageBytes);
            }
        }
        public static Message Receive(BinaryReader reader)
        {
            byte[] messageBytes;
            uint messageLength;
            ushort messageType;
            lock (reader.BaseStream)
            {
                messageType = reader.ReadUInt16();
                messageLength = reader.ReadUInt32();
                messageBytes = reader.ReadBytes((int)messageLength);
            }
            return Message.CreateMessage(messageType, messageBytes);
        }
    }
}
