using System;

namespace ArxLibertatisServer.Messages.Outgoing
{
    /// <summary>
    /// sent and received to trigger a level change
    /// </summary>
    [MessageType(MessageTypeEnum.OutgoingLevelChange)]
    public class OutgoingLevelChange : OutgoingMessage
    {
        public int level;
        //TODO: marker name?

        public override byte[] GetBytes()
        {
            return BitConverter.GetBytes(level);
        }
    }
}
