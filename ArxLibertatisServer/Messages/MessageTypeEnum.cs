namespace ArxLibertatisServer.Messages
{
    enum MessageTypeEnum : ushort
    {
        Handshake = 1,
        AnnounceClientEnter = 2,
        AnnounceClientExit = 3,
        AnnounceServerExit = 4,
        LevelChange = 5,
        ByeBye = 6,
    }
}
