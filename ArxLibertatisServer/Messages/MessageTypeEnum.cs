namespace ArxLibertatisServer.Messages
{
    public enum MessageTypeEnum : ushort
    {
        Handshake = 1,
        AnnounceClientEnter = 2,
        AnnounceClientExit = 3,
        AnnounceServerExit = 4,
        IncomingLevelChange = 5,
        ByeBye = 6,
        IncomingChatMessage = 7,
        OutgoingChatMessage = 8,
        HandshakeAnswer = 9,
        IncomingChangePlayerPosition = 10,
        IncomingChangePlayerOrientation = 11,
        IncomingTriggerPlayerJump = 12,
        OutgoingChangePlayerPosition = 13,
        OutgoingChangePlayerOrientation = 14,
        OutgoingTriggerPlayerJump = 15,
        OutgoingLevelChange = 16,
    }
}
