using System;

namespace ArxLibertatisServer.Messages
{
    public class MessageTypeAttribute : Attribute
    {
        public ushort Type { get; }

        public MessageTypeAttribute(ushort type)
        {
            Type = type;
        }
    }
}
