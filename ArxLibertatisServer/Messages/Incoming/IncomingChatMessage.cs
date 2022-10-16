using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArxLibertatisServer.Messages.Incoming
{
    [MessageType(MessageTypeEnum.IncomingChatMessage)]
    public class IncomingChatMessage : IncomingMessage
    {
        public string message;

        public override void FromBytes(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            message = ReadString(ms);
        }
    }
}
