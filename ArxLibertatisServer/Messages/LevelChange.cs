using System;

namespace ArxLibertatisServer.Messages
{
    /// <summary>
    /// sent and received to trigger a level change
    /// </summary>
    [MessageType((ushort)MessageTypeEnum.LevelChange)]
    public class LevelChange : Message
    {
        public long level;
        //TODO: marker name?

        public override void FromBytes(byte[] bytes)
        {
            level = BitConverter.ToInt64(bytes);
        }

        public override byte[] GetBytes()
        {
            return BitConverter.GetBytes(level);
        }
    }
}
