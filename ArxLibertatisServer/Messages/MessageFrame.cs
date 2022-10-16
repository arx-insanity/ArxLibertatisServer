using ArxLibertatisServer.Messages.Incoming;
using ArxLibertatisServer.Messages.Outgoing;
using NLog;
using System.IO;

namespace ArxLibertatisServer.Messages
{
    public static class MessageFrame
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Send(OutgoingMessage message, BinaryWriter writer)
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
        public static IncomingMessage Receive(BinaryReader reader)
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
            logger.Debug("Got message of type " + messageType + " with body length " + messageLength);
            return Message.CreateMessage(messageType, messageBytes);
        }
    }
}
