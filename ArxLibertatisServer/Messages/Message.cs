using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArxLibertatisServer.Messages
{
    public abstract class Message
    {
        private static readonly Dictionary<Type, ushort> typeToMessageType = new Dictionary<Type, ushort>();
        private static ushort ExtractMessageType(Type t)
        {
            if(typeToMessageType.TryGetValue(t,out ushort messageType))
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

        public virtual ushort GetMessageType()
        {
            return ExtractMessageType(GetType());
        }
        public abstract byte[] GetBytes();
        public abstract void FromBytes(byte[] bytes);

        static readonly Dictionary<ushort, Type> messageTypeToType = new Dictionary<ushort, Type>();

        public static void RegisterMessageType(Type messageType)
        {
            lock (messageTypeToType)
            {
                messageTypeToType[ExtractMessageType(messageType)] = messageType;
            }
        }

        public static Message CreateMessage(ushort messageType, byte[] messageBytes)
        {
            if (!messageTypeToType.TryGetValue(messageType, out Type messageTypeInfo))
            {
                throw new Exception("No message type " + messageType + " registered");
            }
            Message msg = Activator.CreateInstance(messageTypeInfo) as Message;
            msg.FromBytes(messageBytes);
            return msg;
        }

        public static string ReadString(MemoryStream buffer)
        {
            byte[] b = new byte[4];
            buffer.Read(b);
            uint bytesLen = BitConverter.ToUInt32(b);
            b = new byte[bytesLen];
            buffer.Read(b);
            var str = Encoding.UTF8.GetString(b);
            return str;
        }

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
