using System;

namespace ArxLibertatisServer.Messages.Incoming
{
    /// <summary>
    /// received by server when client exits
    /// </summary>
    [MessageType(MessageTypeEnum.ByeBye)]
    public class ByeBye : IncomingMessage
    {
        public override void FromBytes(byte[] bytes)
        { }
    }
}
