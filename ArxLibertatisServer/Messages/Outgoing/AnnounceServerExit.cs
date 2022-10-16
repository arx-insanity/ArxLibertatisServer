using System;

namespace ArxLibertatisServer.Messages.Outgoing
{
    /// <summary>
    /// sent by server to tell clients the server exited
    /// </summary>
    [MessageType(MessageTypeEnum.AnnounceServerExit)]
    public class AnnounceServerExit : OutgoingMessage
    {
        public override byte[] GetBytes()
        {
            return Array.Empty<byte>();
        }
    }
}
