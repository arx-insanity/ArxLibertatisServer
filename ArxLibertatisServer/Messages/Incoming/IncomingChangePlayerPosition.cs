using System;
using System.IO;

namespace ArxLibertatisServer.Messages.Incoming
{
    [MessageType(MessageTypeEnum.IncomingChangePlayerPosition)]
    public class IncomingChangePlayerPosition : IncomingMessage
    {
        public float x;
        public float y;
        public float z;

        public override void FromBytes(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            x = ms.ReadStruct<float>();
            y = ms.ReadStruct<float>();
            z = ms.ReadStruct<float>();
        }
    }
}
