namespace ArxLibertatisServer.Messages
{
    /// <summary>
    /// sent by server to accounce when a client joins the server
    /// </summary>
    [MessageType((ushort)MessageTypeEnum.AnnounceClientEnter)]
    public class AnnounceClientEnter : Handshake
    {
        //basically the same as the Handshake, just that it is sent to clients
    }
}
