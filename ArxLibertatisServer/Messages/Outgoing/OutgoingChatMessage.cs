using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArxLibertatisServer.Messages.Outgoing
{
    [MessageType(MessageTypeEnum.OutgoingChatMessage)]
    public class OutgoingChatMessage : OutgoingMessage
    {
        public Guid senderId;
        public string message;

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            Write(senderId.ToString(), ms);
            Write(message, ms);
            return ms.ToArray();
        }
    }
}
