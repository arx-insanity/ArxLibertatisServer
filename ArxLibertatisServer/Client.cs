using ArxLibertatisServer.Messages;
using NLog;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ArxLibertatisServer
{
    public class Client
    {
        private readonly Server server;
        private readonly TcpClient client;
        private readonly BinaryReader reader;
        private readonly BinaryWriter writer;

        private Thread readerThread = null;
        private bool readerRunning = false;

        public Guid Id
        {
            get;
            private set;
        }
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
            readerThread = new Thread(this.ReadLoop);
            readerThread.Start();
            //TODO should also try pinging the client occasionally to make sure he is still there
        }

        public void Stop()
        {
            readerRunning = false;
            readerThread.Interrupt();
        }

        public void Join()
        {
            readerThread.Join();
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
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Warn(ex, "Error When Receiving Message in Client " + this);
                }
            }
        }

        public void Send(Message message)
        {
            MessageFrame.Send(message, writer);
        }

        private void HandleMessage(Message msg)
        {
            switch (msg)
            {
                case Handshake handshake:
                    Name = handshake.name;
                    Id = handshake.id;
                    var clientEnter = new AnnounceClientEnter
                    {
                        id = Id,
                        name = Name
                    };
                    server.Broadcast(clientEnter, this);
                    lock (server.State)
                    {
                        var levelChangeOnEnter = new LevelChange
                        {
                            level = server.State.level
                        };
                        Send(levelChangeOnEnter);
                    }
                    break;
                case LevelChange levelChange:
                    lock (server.State)
                    {
                        if (server.State.level != levelChange.level)
                        {
                            server.State.level = levelChange.level;
                            server.Broadcast(levelChange, this);
                        }
                    }
                    break;
                case ByeBye _:
                    var clientExit = new AnnounceClientExit
                    {
                        id = Id
                    };
                    server.Broadcast(clientExit, this);
                    server.DisconnectClient(this);
                    break;
            }
        }
    }
}
