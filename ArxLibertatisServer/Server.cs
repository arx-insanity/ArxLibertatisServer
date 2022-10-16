using ArxLibertatisServer.Messages;
using ArxLibertatisServer.Util;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace ArxLibertatisServer
{
    public class Server
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly TcpListenerEx listener;

        private readonly List<Client> clients = new List<Client>();
        public ServerState State
        {
            get;
        } = new ServerState();

        /*
        //TODO: make thread safe way of reading all clients
        public IReadOnlyList<Client> Clients
        {
            get { return clients.; }
        }*/

        public Server(IPEndPoint endpoint)
        {
            listener = new TcpListenerEx(endpoint);
        }

        public void Start()
        {
            if (!listener.Active)
            {
                logger.Info("Starting Server on " + listener.LocalEndpoint);
                listener.Start();
                listener.BeginAcceptSocket(AcceptClient, listener);
            }
        }

        public void Stop()
        {
            if (listener.Active)
            {
                listener.Stop();
            }
        }

        private void AcceptClient(IAsyncResult ar)
        {
            var list = ar.AsyncState as TcpListenerEx;
            var tcpClient = list.EndAcceptTcpClient(ar);
            var client = new Client(this, tcpClient);
            lock (clients)
            {
                clients.Add(client);
            }
            client.Start();
            list.BeginAcceptSocket(AcceptClient, list);
        }

        public void DisconnectClient(Client client)
        {
            lock (clients)
            {
                clients.Remove(client);
            }
            client.Stop();
        }

        public void Join()
        {
            //dirty, but does the job
            while (listener.Active)
            {
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// sends a message to all clients, except one if specified
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exceptFor"></param>
        public void Broadcast(Message message, Client exceptFor = null)
        {
            lock (clients)
            {
                foreach (var client in clients)
                {
                    if (client == exceptFor)
                    {
                        continue;
                    }
                    client.Send(message);
                }
            }
        }
    }
}
