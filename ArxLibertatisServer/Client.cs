using ArxLibertatisServer.Messages;
using ArxLibertatisServer.Messages.Incoming;
using ArxLibertatisServer.Messages.Outgoing;
using NLog;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ArxLibertatisServer
{
    public class Client
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly Server server;
        private readonly TcpClient client;
        private readonly BinaryReader reader;
        private readonly BinaryWriter writer;

        private Thread readerThread = null;
        private bool readerRunning = false;

        public Guid Id
        {
            get;
        } = Guid.NewGuid();
        public string Name
        {
            get;
            private set;
        }

        public Client(Server server, TcpClient tcpClient)
        {
            this.server = server;
            this.client = tcpClient;
            var stream = client.GetStream();
            this.reader = new BinaryReader(stream);
            this.writer = new BinaryWriter(stream);
        }

        public void Start()
        {
            if (readerThread == null)
            {
                readerThread = new Thread(this.ReadLoop);
                readerRunning = true;
                readerThread.Start();
                //TODO should also try pinging the client occasionally to make sure he is still there
            }
        }

        public void Stop()
        {
            if (readerThread != null)
            {
                readerRunning = false;
                readerThread.Interrupt();
            }
        }

        public void Join()
        {
            if (readerThread != null)
            {
                readerThread.Join();
                readerThread = null;
            }
        }

        private void ReadLoop()
        {
            while (readerRunning)
            {
                try
                {
                    Message msg = MessageFrame.Receive(reader);
                    HandleMessage(msg);
                }
                catch (ThreadInterruptedException)
                {
                    break;
                }
                catch (IOException)
                {

                    //remote closed socket
                    logger.Info("Client " + Id + " left");
                    server.DisconnectClient(this);
                    break;
                }
                catch (Exception ex)
                {
                    logger.Warn(ex, "Error When Receiving Message in Client " + this);
                }
            }
        }

        public void Send(OutgoingMessage message)
        {
            try
            {
                MessageFrame.Send(message, writer);
            }
            catch (IOException)
            {
                //TODO: what now?
            }
        }

        private void HandleMessage(Message msg)
        {
            switch (msg)
            {
                case Handshake handshake:
                    logger.Info("Got Handshake, playername: " + handshake.name + " from " + Id);
                    Name = handshake.name;
                    logger.Info("Sending handshake answer " + Id);
                    var answer = new HandshakeAnswer
                    {
                        id = Id
                    };
                    Send(answer);
                    logger.Info("Broadcasting client enter");
                    var clientEnter = new AnnounceClientEnter
                    {
                        id = Id,
                        name = Name
                    };
                    server.Broadcast(clientEnter, this);
                    logger.Info("Sending Levelchange on join");
                    lock (server.State)
                    {
                        var levelChangeOnEnter = new OutgoingLevelChange
                        {
                            level = server.State.level
                        };
                        Send(levelChangeOnEnter);
                    }
                    break;
                case IncomingLevelChange levelChange:
                    logger.Info("Got LevelChange to: " + levelChange.level + " from " + Id);
                    lock (server.State)
                    {
                        if (server.State.level != levelChange.level)
                        {
                            server.State.level = levelChange.level;
                            var levelChangeOut = new OutgoingLevelChange
                            {
                                level = levelChange.level
                            };
                            server.Broadcast(levelChangeOut, this);
                        }
                    }
                    break;
                case ByeBye _:
                    logger.Info("Got ByeBye from " + Id);
                    server.DisconnectClient(this);
                    break;
                case IncomingChatMessage incomingChatMessage:
                    logger.Info("Got Chat Message from " + Id);
                    var chatMessage = new OutgoingChatMessage
                    {
                        senderId = Id,
                        message = incomingChatMessage.message
                    };
                    server.Broadcast(chatMessage, this);
                    break;
                default:
                    logger.Warn("unhandled message " + msg);
                    break;
            }
        }
    }
}
