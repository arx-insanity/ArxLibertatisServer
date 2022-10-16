using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArxLibertatisServer.Messages
{
    public abstract class Message
    {
        private static readonly Dictionary<ushort, Type> messageTypeToType = new Dictionary<ushort, Type>();
        private static readonly Dictionary<Type, ushort> typeToMessageType = new Dictionary<Type, ushort>();
        public static void RegisterMessageType(Type messageType)
        {
            lock (messageTypeToType)
            {
                messageTypeToType[ExtractMessageType(messageType)] = messageType;
            }
        }
        private static ushort ExtractMessageType(Type t)
        {
            if (typeToMessageType.TryGetValue(t, out ushort messageType))
            {
                return messageType;
            }
            var custAtts = t.GetCustomAttributes(false);
            foreach (var custAtt in custAtts)
            {
                if (custAtt is MessageTypeAttribute)
                {
                    return (custAtt as MessageTypeAttribute).Type;
                }
            }
            throw new Exception("This type does not have a " + nameof(MessageTypeAttribute));
        }

        private ushort messageType = 0;
        public virtual ushort GetMessageType()
        {
            if (messageType == 0)
            {
                messageType = ExtractMessageType(GetType());
            }
            return messageType;
        }

        public static Incoming.IncomingMessage CreateMessage(ushort messageType, byte[] messageBytes)
        {
            if (!messageTypeToType.TryGetValue(messageType, out Type messageTypeInfo))
            {
                throw new Exception("No message type " + messageType + " registered");
            }
            Incoming.IncomingMessage msg = Activator.CreateInstance(messageTypeInfo) as Incoming.IncomingMessage;
            msg.FromBytes(messageBytes);
            return msg;
        }
    }
}
