using System;

namespace ArxLibertatisServer.Messages.Incoming
{
    /// <summary>
    /// sent and received to trigger a level change
    /// </summary>
    [MessageType(MessageTypeEnum.IncomingLevelChange)]
    public class IncomingLevelChange : IncomingMessage
    {
        public int level;
        //TODO: marker name?

        public override void FromBytes(byte[] bytes)
        {
            level = BitConverter.ToInt32(bytes);
        }
    }
}
