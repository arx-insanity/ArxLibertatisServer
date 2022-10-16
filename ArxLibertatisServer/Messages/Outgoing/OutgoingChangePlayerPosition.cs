using System;
using System.IO;

namespace ArxLibertatisServer.Messages.Outgoing
{
    [MessageType(MessageTypeEnum.OutgoingChangePlayerPosition)]
    public class OutgoingChangePlayerPosition : OutgoingMessage
    {
        public Guid id;
        public float x;
        public float y;
        public float z;

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            Write(id.ToString(), ms);
            ms.WriteStruct(x);
            ms.WriteStruct(y);
            ms.WriteStruct(z);
            return ms.ToArray();
        }
    }
}
